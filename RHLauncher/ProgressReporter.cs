using System.Diagnostics;

namespace RHLauncher.RHLauncher
{
    internal class ProgressReporter
    {
        public static void ReportInstallProgress(IProgress<ProgressReport> progress, string label, string file, int pos, int count, CancellationToken cancellationToken)
        {
            ProgressReport report = new()
            {
                Panel = true,
                ShowFileNameLabel = true,
                ShowSpeedLabel = false,
                ShowTimeLabel = false,
                ShowFileSizeLabel = false,
                ShowFileCountLabel = true,
                Label = label,
                FileName = Path.GetFileName(file),
                PercentComplete = (int)Math.Round(pos * 100.0 / count),
                NumFilesPacked = pos,
                TotalNumFiles = count,
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
                ShowFileNameLabel = true,
                ShowSpeedLabel = true,
                ShowTimeLabel = true,
                ShowFileSizeLabel = true,
                ShowFileCountLabel = true,
                Label = LocalizedStrings.Downloading,
                PercentComplete = percentComplete,
                FileName = fileName,
                BytesDownloaded = downloadedSize,
                TotalBytesToDownload = totalBytesToDownload,
                TotalSpeed = totalSpeed,
                TimeLeft = timeLeft,
                NumFilesDownloaded = downloadedFileCount,
                TotalNumFiles = totalFilesToDownload,
                CancellationToken = cancellationToken
            };

            progress.Report(report);
        }

        public static void ReportErrorProgress(IProgress<ProgressReport> progress, string message, CancellationToken cancellationToken)
        {
            ProgressReport report = new()
            {
                Panel = true,
                ShowFileNameLabel = false,
                ShowSpeedLabel = false,
                ShowTimeLabel = false,
                ShowFileSizeLabel = false,
                ShowFileCountLabel = false,
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
                CancellationToken = cancellationToken
            };

            progress.Report(report);
        }
    }
}
