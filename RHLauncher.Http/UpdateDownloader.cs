using RHLauncher.RHLauncher.Helper;
using RHLauncher.RHLauncher.i8n;
using RHLauncher.RHLauncher.PCK;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace RHLauncher.RHLauncher.Http
{
    public class UpdateDownloader
    {
        public enum UpdateState
        {
            UpdateAvailable,
            NoUpdateAvailable,
            Error
        }

        private const int BufferSize = 8192;
        private const int TimeoutSeconds = 30;

        public static async Task<UpdateState> CheckForUpdatesAsync()
        {
            try
            {
                using var client = new HttpClient();

                string fileListUrl = Configuration.Default.FileListUrl;

                using HttpResponseMessage filelistResponse = await client.GetAsync(fileListUrl);

                if (!filelistResponse.IsSuccessStatusCode)
                {
                    // Handle the case where the filelist URL is not found
                    return UpdateState.Error;
                }

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

        public static async Task DownloadUpdatesAsync(string installDirectory, IProgress<ProgressReport> progress, CancellationToken cancellationToken)
        {
            using HttpClientHandler handler = new() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            using HttpClient client = new(handler, true) { Timeout = TimeSpan.FromSeconds(TimeoutSeconds) };

            try
            {
                string archiveDir = Path.Combine(installDirectory, "archive");
                if (!Directory.Exists(archiveDir))
                {
                    Directory.CreateDirectory(archiveDir);
                }

                string filelistUrl = Configuration.Default.FileListUrl;
                string filelistText = await client.GetStringAsync(filelistUrl, cancellationToken).ConfigureAwait(false);

                List<PCKFile> fileList = PCKReader.GetPCKFileList();
                Dictionary<string, uint> fileHashes = fileList.ToDictionary(f => f.Name, f => f.Hash);

                HashSet<string> localFileNames = new(fileHashes.Keys);

                List<string> filesToBeDownloaded = new();
                long totalBytesToDownload = 0L;
                int totalFilesToDownload = 0;

                List<string> lines = filelistText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                int totalLines = lines.Count;

                for (int i = 0; i < totalLines; i++)
                {
                    string line = lines[i];
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
                        long fileSize = await DownloadHelper.GetFileSizeAsync(client, $"{Configuration.Default.DownloadUpdateFileUrl}/{fileName}.mip", cancellationToken).ConfigureAwait(false);
                        totalBytesToDownload += fileSize;
                        totalFilesToDownload++;
                    }

                    ProgressReporter.ReportFileListCheckProgress(progress, i + 1, totalLines, cancellationToken);
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

                    string? directoryPath = Path.GetDirectoryName(filePath);

                    if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
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
                    downloadedFileCount++;
                }
                fileHashes.Clear();

                ProgressReporter.ReportFinishedProgress(progress, cancellationToken);
            }
            catch (OperationCanceledException ex)
            {
                Logger.WriteLog($"Update download cancelled: {ex.Message}");
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

    }
}