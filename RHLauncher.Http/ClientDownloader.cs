using Ionic.Zip;
using RHLauncher.RHLauncher.Helper;
using RHLauncher.RHLauncher.i8n;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace RHLauncher.RHLauncher.Http
{
    public class ClientDownloader
    {
        public enum DownloadClientResultStatus
        {
            Success,
            Failed,
            Cancelled
        }

        public class DownloadClientResult
        {
            public DownloadClientResultStatus Status { get; set; }
            public string? InstallDirectoryPath { get; set; }
        }

        private const int BufferSize = 8192;
        private const int TimeoutSeconds = 30;

        public static async Task<DownloadClientResult> DownloadClientAsync(string installDirectory, IProgress<ProgressReport> progress, CancellationToken cancellationToken)
        {
            using HttpClientHandler handler = new() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            using HttpClient client = new(handler, true) { Timeout = TimeSpan.FromSeconds(TimeoutSeconds) };

            try
            {
                string filelistUrl = Configuration.Default.ClientPartsListUrl;
                using HttpResponseMessage filelistResponse = await client.GetAsync(filelistUrl, cancellationToken);

                if (!filelistResponse.IsSuccessStatusCode)
                {
                    // Handle the case where the filelist URL is not found
                    string errorMessage = $"{LocalizedStrings.ClientDownloadErrorMessage}:\n\n{LocalizedStrings.ClientDownloadFilelistError}: {filelistUrl}.\n\n {LocalizedStrings.Error}: {filelistResponse.ReasonPhrase}";
                    MsgBoxForm.Show($" {LocalizedStrings.Error}: {errorMessage}", LocalizedStrings.Error);
                    Logger.WriteLog(errorMessage);
                    return new DownloadClientResult { Status = DownloadClientResultStatus.Failed };
                }
                string filelistText = await client.GetStringAsync(filelistUrl, cancellationToken).ConfigureAwait(false);

                string downloadDir = Path.Combine(installDirectory, "rhclient_download");
                if (!Directory.Exists(downloadDir))
                {
                    Directory.CreateDirectory(downloadDir);
                }

                List<string> filesToBeDownloaded = new();
                long totalBytesToDownload = 0L;

                List<string> lines = filelistText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                int totalLines = lines.Count;
                int checkedFiles = 0;

                foreach (string line in lines)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string[] parts = line.Split(' ');
                    if (parts.Length != 3)
                    {
                        continue;
                    }

                    string fileName = parts[0];
                    long fileSize = long.Parse(parts[1]);
                    ulong fileHashFromServer = ulong.Parse(parts[2], NumberStyles.HexNumber, CultureInfo.InvariantCulture);

                    string filePath = Path.Combine(downloadDir, fileName);
                    if (File.Exists(filePath))
                    {
                        string existingFileHash = Crc32.GetFileHash(filePath);
                        if (existingFileHash.Equals(fileHashFromServer.ToString("X8"), StringComparison.OrdinalIgnoreCase))
                        {
                            checkedFiles++;
                            ProgressReporter.ReportFileCheckingProgress(progress, checkedFiles, totalLines, cancellationToken);
                            continue; // Skip this file as it already exists and the hash matches
                        }
                    }

                    filesToBeDownloaded.Add(fileName);
                    totalBytesToDownload += fileSize;
                    checkedFiles++;
                    ProgressReporter.ReportFileCheckingProgress(progress, checkedFiles, totalLines, cancellationToken);
                }

                Stopwatch sw = Stopwatch.StartNew();
                long downloadedBytes = 0L;

                foreach (string fileName in filesToBeDownloaded)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    string fileUrl = $"{Configuration.Default.DownloadClientPartUrl}/{fileName}";
                    string filePath = Path.Combine(downloadDir, fileName);

                    using HttpResponseMessage response = await client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    await using Stream contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                    await using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, BufferSize, true);

                    byte[] buffer = new byte[BufferSize];
                    int bytesRead;
                    while ((bytesRead = await contentStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false)) > 0)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken).ConfigureAwait(false);

                        downloadedBytes += bytesRead;
                        ProgressReporter.ReportClientDownloadProgress(progress, downloadedBytes, totalBytesToDownload, sw, cancellationToken);
                    }
                }
                ProgressReporter.ReportFinishedProgress(progress, cancellationToken);
                string rhExePath = await UnzipFilesAsync(downloadDir, installDirectory, progress, cancellationToken);
                ProgressReporter.ReportFinishedProgress(progress, cancellationToken);
                return new DownloadClientResult
                {
                    Status = DownloadClientResultStatus.Success,
                    InstallDirectoryPath = rhExePath
                };

            }
            catch (OperationCanceledException ex)
            {
                Logger.WriteLog($"Download cancelled: {ex.Message}");
                return new DownloadClientResult { Status = DownloadClientResultStatus.Cancelled };
            }
            catch (Exception ex)
            {
                ProgressReporter.ReportErrorProgress(progress, ex.Message, cancellationToken);
                string errorMessage = LocalizedStrings.ClientDownloadError + "\n" + ex.Message;
                string errorLog = LocalizedStrings.ClientDownloadError + ex.Message + ex.StackTrace;
                Exception newEx = new(errorMessage, ex);
                Exception newLogEx = new(errorLog, ex);
                ExceptionHandler.HandleException(newEx, newLogEx);
                return new DownloadClientResult { Status = DownloadClientResultStatus.Failed };
            }
        }

        private static async Task<string> UnzipFilesAsync(string sourceDirectory, string destinationDirectory, IProgress<ProgressReport> progress, CancellationToken cancellationToken)
        {
            string[] fileEntries = Directory.GetFiles(sourceDirectory, "*.zip.*").OrderBy(f => f).ToArray();
            string rhExeDirectoryPath = "";

            // Calculate total uncompressed size of all files in all zip archives
            long totalUncompressedSize = fileEntries.Sum(file =>
            {
                using ZipFile zip = ZipFile.Read(file);
                return zip.Entries.Sum(entry => entry.UncompressedSize);
            });
            long totalExtracted = 0;

            try
            {
                foreach (string file in fileEntries)
                {
                    await Task.Run(() =>
                    {
                        using ZipFile zip = ZipFile.Read(file);
                        foreach (var entry in zip)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            string destinationPath = Path.Combine(destinationDirectory, entry.FileName);
                            string? directoryPath = Path.GetDirectoryName(destinationPath);
                            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            entry.Extract(destinationDirectory, ExtractExistingFileAction.OverwriteSilently);
                            totalExtracted += entry.UncompressedSize;
                            cancellationToken.ThrowIfCancellationRequested();

                            ProgressReporter.ReportUnzipProgress(progress, totalExtracted, totalUncompressedSize, cancellationToken);

                            // Check if the current entry is "rustyhearts.exe" and store its directory path
                            if (entry.FileName.EndsWith("rustyhearts.exe", StringComparison.OrdinalIgnoreCase))
                            {
                                rhExeDirectoryPath = Path.GetDirectoryName(destinationPath) ?? "";
                            }
                        }
                    }, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }

            try
            {
                if (Directory.Exists(sourceDirectory))
                {
                    Directory.Delete(sourceDirectory, true);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{LocalizedStrings.ClientFolderErrorMessage}", ex);
            }

            // Check if 'rustyhearts.exe' directory exists
            if (!string.IsNullOrWhiteSpace(rhExeDirectoryPath) && Directory.Exists(rhExeDirectoryPath))
            {
                return rhExeDirectoryPath;
            }
            else
            {
                throw new FileNotFoundException($"{LocalizedStrings.ClientFolderExeErrorMessage}");
            }
        }


    }
}
