using RHLauncher.Helper;
using RHLauncher.PCK;
using RHLauncher.RHLauncher.Helper;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace RHLauncher.RHLauncher
{
    public class UpdateDownloader
    {
        private const int BufferSize = 8192;
        private const int TimeoutSeconds = 30;

        public static async Task<UpdateState> CheckForUpdatesAsync()
        {
            try
            {
                string fileListUrl = Configuration.Default.FileListUrl;
                using var client = new HttpClient();
                using var response = await client.GetAsync(fileListUrl);
                using var content = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(content);
                string filelistText = await reader.ReadToEndAsync();

                List<PCKFile> fileList = PCKReader.GetPCKFileList();
                Dictionary<string, uint> fileHashes = fileList.ToDictionary(f => f.Name, f => f.Hash);

                List<string> filesToUpdate = new();

                foreach (string line in filelistText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length != 3)
                    {
                        continue;
                    }

                    string fileName = parts[0];
                    if (fileName.Contains(' '))
                    {
                        fileName = $"\"{fileName}\"";
                    }

                    if (!ulong.TryParse(parts[2], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ulong fileHashFromServer))
                    {
                        continue;
                    }

                    if (!fileHashes.TryGetValue(fileName, out uint fileHash) || fileHash != fileHashFromServer)
                    {
                        filesToUpdate.Add(fileName);
                    }
                }

                fileHashes.Clear();

                return filesToUpdate.Count > 0 ? UpdateState.UpdateAvailable : UpdateState.NoUpdateAvailable;
            }
            catch (Exception ex)
            {
                string errorMessage = LocalizedStrings.UpdateCheckError + "\n" + ex.Message;
                string errorLog = LocalizedStrings.UpdateCheckError + ex.Message + ex.StackTrace;
                Exception newEx = new(errorMessage, ex);
                Exception newLogEx = new(errorLog, ex);
                ExceptionHandler.HandleException(newEx, newLogEx);

                return UpdateState.Error;
            }
        }

        public enum UpdateState
        {
            UpdateAvailable,
            NoUpdateAvailable,
            Error
        }

        public static async Task DownloadUpdatesAsync(string installDirectory, IProgress<ProgressReport> progress, CancellationToken cancellationToken)
        {
            using HttpClientHandler handler = new() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            using HttpClient client = new(handler, true) { Timeout = TimeSpan.FromSeconds(TimeoutSeconds) };

            try
            {
                string filelistUrl = Configuration.Default.FileListUrl;
                string filelistText = await client.GetStringAsync(filelistUrl, cancellationToken).ConfigureAwait(false);

                List<PCKFile> fileList = PCKReader.GetPCKFileList();
                Dictionary<string, uint> fileHashes = fileList.ToDictionary(f => f.Name, f => f.Hash);

                HashSet<string> localFileNames = new(fileHashes.Keys);

                List<string> filesToBeDownloaded = new();
                long totalBytesToDownload = 0L;
                int totalFilesToDownload = 0;
                foreach (string line in filelistText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string[] parts = line.Split(' ');
                    if (parts.Length != 3)
                    {
                        continue;
                    }

                    string fileName = parts[0];
                    if (string.IsNullOrWhiteSpace(parts[0]))
                    {
                        fileName = $"\"{fileName}\"";
                    }

                    if (!ulong.TryParse(parts[2], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ulong fileHashFromServer))
                    {
                        continue;
                    }

                    if (!fileHashes.TryGetValue(fileName, out uint fileHash) || fileHash != fileHashFromServer)
                    {
                        filesToBeDownloaded.Add(fileName);
                        long fileSize = await GetFileSizeAsync(client, $"{Configuration.Default.DownloadUpdateFileUrl}/{fileName}.mip", cancellationToken).ConfigureAwait(false);
                        totalBytesToDownload += fileSize;
                        totalFilesToDownload++;
                    }
                }

                string archiveDir = Path.Combine(installDirectory, "archive");
                if (!Directory.Exists(archiveDir))
                {
                    Directory.CreateDirectory(archiveDir);
                }

                Stopwatch sw = Stopwatch.StartNew();

                long downloadedBytes = 0L;
                int downloadedSize = 0;
                int downloadedFileCount = 0;
                foreach (string fileName in filesToBeDownloaded)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string finalfileName = fileName + ".mip";
                    string fileUrl = $"{Configuration.Default.DownloadUpdateFileUrl}/{finalfileName}";
                    string filePath = Path.Combine(archiveDir, finalfileName);

                    using HttpResponseMessage response = await client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    await using Stream contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    }

                    await using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, BufferSize, true);
                    byte[] buffer = new byte[BufferSize];
                    int bytesRead;
                    while ((bytesRead = await contentStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false)) > 0)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken).ConfigureAwait(false);

                        downloadedBytes += bytesRead;
                        downloadedSize += bytesRead;
                    }

                    ProgressReporter.ReportDownloadProgress(progress, finalfileName, downloadedSize, totalBytesToDownload, downloadedFileCount, totalFilesToDownload, sw, cancellationToken);
                    await Task.Yield();
                    downloadedFileCount++;
                }
                fileHashes.Clear();

                ProgressReporter.ReportFinishedProgress(progress, cancellationToken);
            }
            catch (OperationCanceledException ex)
            {
                Logger.WriteLog($"Download update cancelled: {ex.Message}");
            }
            catch (Exception ex)
            {
                ProgressReporter.ReportErrorProgress(progress, ex.Message, cancellationToken);

                string errorMessage = LocalizedStrings.UpdateDownloadError + "\n" + ex.Message;
                string errorLog = LocalizedStrings.UpdateDownloadError + ex.Message + ex.StackTrace;
                Exception newEx = new(errorMessage, ex);
                Exception newLogEx = new(errorLog, ex);
                ExceptionHandler.HandleException(newEx, newLogEx);
            }
        }

        private static async Task<long> GetFileSizeAsync(HttpClient client, string fileUrl, CancellationToken cancellationToken)
        {
            using HttpRequestMessage request = new(HttpMethod.Get, fileUrl);
            using HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return response.Content.Headers.ContentLength ?? throw new Exception("Content-Length header not found in response headers.");
        }

        public static string FormatDownloadSpeed(double totalDownloadSpeed)
        {
            string[] sizes = { "B/s", "KB/s", "MB/s", "GB/s" };
            int order = 0;
            while (totalDownloadSpeed >= 1024 && order < sizes.Length - 1)
            {
                order++;
                totalDownloadSpeed /= 1024;
            }

            return $"{totalDownloadSpeed:0.#} {sizes[order]}";
        }

        public static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (bytes >= 1024 && order < sizes.Length - 1)
            {
                order++;
                bytes /= 1024;
            }

            return $"{bytes:0.#} {sizes[order]}";
        }
    }
}