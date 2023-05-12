using RHLauncher.Helper;
using RHLauncher.PCK;

namespace RHLauncher.RHLauncher
{
    public class UpdateInstaller
    {
        public static async Task PackDownloadedFilesAsync(string installDir, IProgress<ProgressReport> progress, CancellationToken cancellationToken)
        {
            string archiveDir = Path.Combine(installDir, "archive");
            string updateRootFolder = Path.Combine(installDir, "Update");

            if (!Directory.Exists(updateRootFolder))
            {
                return;
            }

            List<string> fileList = Directory.EnumerateFiles(updateRootFolder, "*", SearchOption.AllDirectories).ToList();
            List<PCKFile> existingFiles = PCKReader.GetPCKFileList();
            int count = fileList.Count;
            int pos = 0;

            try
            {
                await PCKWriter.Packing(updateRootFolder, fileList, existingFiles, true,
                    (string fileName, int curPos, int totalCount) =>
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        pos++;
                        ProgressReporter.ReportInstallProgress(progress, LocalizedStrings.Packing, fileName, pos, count, cancellationToken);
                    },
                    cancellationToken
                ).ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                if (Directory.Exists(archiveDir))
                {
                    Directory.Delete(archiveDir, true);
                    Directory.CreateDirectory(archiveDir);
                }

                if (Directory.Exists(updateRootFolder))
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    Directory.Delete(updateRootFolder, true);
                }
                Logger.WriteLog($"Update installation canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"An error occurred while packing: {ex.Message}");
                ProgressReporter.ReportErrorProgress(progress, ex.Message, cancellationToken);
                throw;
            }
            finally
            {
                fileList.Clear();
                existingFiles.Clear();
            }

            ProgressReporter.ReportFinishedProgress(progress, cancellationToken);

            // Pause for a short time to allow other tasks to run
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

            if (Directory.Exists(archiveDir))
            {
                Directory.Delete(archiveDir, true);
                Directory.CreateDirectory(archiveDir);
            }

            if (Directory.Exists(updateRootFolder))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Directory.Delete(updateRootFolder, true);
            }
        }

    }
}
