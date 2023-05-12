using RHLauncher.Helper;

namespace RHLauncher.RHLauncher
{
    public class UpdateUnpacker
    {
        public static async Task UnpackDownloadedFilesAsync(string installDir, IProgress<ProgressReport> progress, CancellationToken cancellationToken)
        {
            if (installDir == null)
                throw new ArgumentNullException(nameof(installDir));
            if (progress == null)
                throw new ArgumentNullException(nameof(progress));

            string archiveDir = Path.Combine(installDir, "archive");
            string updateRootFolder = Path.Combine(installDir, "Update");

            if (!Directory.Exists(archiveDir))
            {
                return;
            }

            if (!Directory.Exists(updateRootFolder))
            {
                Directory.CreateDirectory(updateRootFolder);
            }

            List<string> files = Directory.EnumerateFiles(archiveDir, "*.mip", SearchOption.AllDirectories)
                .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
                .ToList();
            int count = files.Count;
            int pos = 0;

            await Task.Run(async () =>
            {
                foreach (string file in files)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string relativePath = Path.GetRelativePath(archiveDir, file);
                    string outputPath = Path.Combine(updateRootFolder, relativePath.Substring(0, relativePath.Length - 4)); // Remove the ".mip" extension

                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
                        await MIPDecoder.DecompressFileAsync(file, outputPath, cancellationToken);

                        pos++;
                        ProgressReporter.ReportInstallProgress(progress, LocalizedStrings.Unpacking, file, pos, count, cancellationToken);
                    }
                    catch (OperationCanceledException ex)
                    {
                        Logger.WriteLog($"Update unpack canceled: {ex.Message}");
                    }
                    catch (ArgumentException ex)
                    {
                        Logger.WriteLog($"An error occurred while unpacking: {file} {ex}");
                        ProgressReporter.ReportErrorProgress(progress, ex.Message, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog($"Error: {ex.Message} {ex.StackTrace} ");
                        throw;
                    }
                }

                ProgressReporter.ReportFinishedProgress(progress, cancellationToken);
            }, cancellationToken);
        }

        


    }
}
