using RHLauncher.RHLauncher.i8n;
using System.Diagnostics;

namespace RHLauncher.RHLauncher.Helper
{
    public class ProgressReporter
    {
        public static void ReportFileListCheckProgress(IProgress<ProgressReport> progress, int checkedFilesCount, int totalFiles, CancellationToken cancellationToken)
        {
            int percentComplete = totalFiles > 0 ? (int)Math.Round(checkedFilesCount * 100.0 / totalFiles) : 0;

            ProgressReport report = new()
            {
                Panel = true,
                CancellButton = true,
                ShowFileCountLabel = true,
                IsCheckingFilelist = true,
                Button = LocalizedStrings.Checking,
                Label = LocalizedStrings.CheckingFiles,
                Current = checkedFilesCount,
                Total = totalFiles,
                PercentComplete = percentComplete,
                NumFiles = checkedFilesCount,
                TotalNumFiles = totalFiles,
                CancellationToken = cancellationToken
            };

            progress.Report(report);
        }

        public static void ReportDownloadProgress(IProgress<ProgressReport> progress, string fileName, long downloadedSize, long totalBytesToDownload, int downloadedFileCount, int totalFilesToDownload, Stopwatch sw, CancellationToken cancellationToken)
        {
            int percentComplete = totalBytesToDownload > 0 ? (int)Math.Round(downloadedSize * 100.0 / totalBytesToDownload) : 0;
            double totalSpeed = downloadedSize / sw.Elapsed.TotalSeconds;
            long timeLeft = totalBytesToDownload > 0 ? (long)((totalBytesToDownload - downloadedSize) / (downloadedSize / sw.Elapsed.TotalSeconds)) : 0;

            ProgressReport report = new()
            {
                Panel = true,
                CancellButton = true,
                ShowFileNameLabel = true,
                ShowSpeedLabel = true,
                ShowTimeLabel = true,
                ShowFileSizeLabel = true,
                ShowFileCountLabel = true,
                Button = LocalizedStrings.Downloading,
                Label = LocalizedStrings.Downloading,
                PercentComplete = percentComplete,
                FileName = fileName,
                Current = downloadedSize,
                Total = totalBytesToDownload,
                TotalSpeed = totalSpeed,
                TimeLeft = timeLeft,
                NumFiles = downloadedFileCount,
                TotalNumFiles = totalFilesToDownload,
                CancellationToken = cancellationToken
            };

            progress.Report(report);
        }

        public static void ReportClientDownloadProgress(IProgress<ProgressReport> progress, long downloadedSize, long totalBytesToDownload, Stopwatch sw, CancellationToken cancellationToken)
        {
            int percentComplete = totalBytesToDownload > 0 ? (int)Math.Round(downloadedSize * 100.0 / totalBytesToDownload) : 0;
            double totalSpeed = downloadedSize / sw.Elapsed.TotalSeconds;
            long timeLeft = totalBytesToDownload > 0 ? (long)((totalBytesToDownload - downloadedSize) / (downloadedSize / sw.Elapsed.TotalSeconds)) : 0;

            ProgressReport report = new()
            {
                Panel = true,
                CancellButton = true,
                ShowSpeedLabel = true,
                ShowTimeLabel = true,
                ShowFileSizeLabel = true,
                Button = LocalizedStrings.Downloading,
                Label = LocalizedStrings.Downloading,
                PercentComplete = percentComplete,
                Current = downloadedSize,
                Total = totalBytesToDownload,
                TotalSpeed = totalSpeed,
                TimeLeft = timeLeft,
                CancellationToken = cancellationToken
            };

            progress.Report(report);
        }

        public static void ReportFileCheckingProgress(IProgress<ProgressReport> progress, int checkedFiles, int totalFiles, CancellationToken cancellationToken)
        {
            int percentComplete = totalFiles > 0 ? (int)Math.Round(checkedFiles * 100.0 / totalFiles) : 0;

            ProgressReport report = new()
            {
                Panel = true,
                IsCheckingFilelist = true,
                Button = LocalizedStrings.Checking,
                Label = LocalizedStrings.CheckingFiles,
                PercentComplete = percentComplete,
                Current = checkedFiles,
                Total = totalFiles,
                CancellationToken = cancellationToken
            };

            progress.Report(report);
        }

        public static void ReportUnzipProgress(IProgress<ProgressReport> progress, long extractedSize, long totalUncompressedSize, CancellationToken cancellationToken)
        {
            int percentComplete = totalUncompressedSize > 0 ? (int)(extractedSize * 100 / totalUncompressedSize) : 0;

            ProgressReport report = new()
            {
                Panel = true,
                CancellButton = true,
                Button = LocalizedStrings.Installing,
                Label = LocalizedStrings.Installing,
                PercentComplete = percentComplete,
                Current = extractedSize,
                Total = totalUncompressedSize,
                CancellationToken = cancellationToken
            };

            progress.Report(report);
        }

        public static void ReportErrorProgress(IProgress<ProgressReport> progress, string message, CancellationToken cancellationToken)
        {
            ProgressReport report = new()
            {
                Panel = true,
                Button = LocalizedStrings.Error,
                Label = LocalizedStrings.Error,
                ErrorMessage = message,
                CancellationToken = cancellationToken
            };

            progress.Report(report);
        }

        public static void ReportFinishedProgress(IProgress<ProgressReport> progress, CancellationToken cancellationToken)
        {
            ProgressReport report = new()
            {
                Panel = false,
                Button = LocalizedStrings.Launch,
                CancellationToken = cancellationToken
            };

            progress.Report(report);
        }
    }
}
