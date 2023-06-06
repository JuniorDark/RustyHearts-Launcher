using Newtonsoft.Json;
using RHLauncher.RHLauncher;
using RHLauncher.RHLauncher.Helper;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;

namespace RHLauncher
{
    public class LauncherUpdater
    {
        private readonly string LauncherVersionUrl = Configuration.Default.GetLauncherVersion;
        private readonly string LauncherUpdateUrl = Configuration.Default.UpdateLauncherVersion;

        public async Task CheckForLauncherUpdateAsync()
        {
            try
            {
                using HttpClient client = new();
                using HttpResponseMessage response = await client.GetAsync(LauncherVersionUrl);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(json);
                string version = result.version;
                if (!string.IsNullOrEmpty(version))
                {
                    string currentVersion = GetLauncherVersion();
                    if (!string.IsNullOrEmpty(currentVersion) && !currentVersion.Equals(version, StringComparison.OrdinalIgnoreCase))
                    {
                        MsgBoxForm.Show(LocalizedStrings.LauncherUpdateText, LocalizedStrings.Info);
                        await DownloadLauncherUpdateAsync(json);
                    }
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = LocalizedStrings.LauncherUpdateCheckFailed + ex.Message;
                string errorLog = LocalizedStrings.LauncherUpdateCheckFailed + ex.Message + ex.StackTrace;
                Exception newEx = new(errorMessage, ex);
                Exception newLogEx = new(errorLog, ex);
                ExceptionHandler.HandleException(newEx, newLogEx);
            }
        }

        private async Task DownloadLauncherUpdateAsync(string version)
        {
            try
            {
                using HttpClient client = new();
                using StringContent content = new(version, Encoding.UTF8, "application/json");

                dynamic result = JsonConvert.DeserializeObject(version);
                string v = result.version;

                using HttpResponseMessage response = await client.PostAsync(LauncherUpdateUrl, content);
                response.EnsureSuccessStatusCode();
                byte[] updateBytes = await response.Content.ReadAsByteArrayAsync();
                string tempFilePath = Path.Combine(Path.GetTempPath(), $"launcher_update.zip");
                File.WriteAllBytes(tempFilePath, updateBytes);

                string executablePath = AppDomain.CurrentDomain.BaseDirectory;
                string backupFilePath = Path.Combine(executablePath, $"Launcher_old.exe");
                string launcherExePath = Path.Combine(executablePath, "Launcher.exe");

                if (File.Exists(backupFilePath))
                {
                    File.Delete(backupFilePath);
                }

                // Move existing launcher.exe to temporary location
                File.Move(launcherExePath, backupFilePath);

                // Extract new update files
                ZipFile.ExtractToDirectory(tempFilePath, executablePath, true);

                // Delete temporary zip file
                File.Delete(tempFilePath);

                MsgBoxForm.Show(LocalizedStrings.LauncherUpdateSuccess +  v, LocalizedStrings.Info);
                Application.Restart();
            }
            catch (Exception ex)
            {
                string errorMessage = LocalizedStrings.LauncherUpdateFailed + ex.Message;
                string errorLog = LocalizedStrings.LauncherUpdateFailed + ex.Message + ex.StackTrace;
                Exception newEx = new(errorMessage, ex);
                Exception newLogEx = new(errorLog, ex);
                ExceptionHandler.HandleException(newEx, newLogEx);
            }
        }

        public static string GetLauncherVersion()
        {
            // Get the version information of the application
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

            // Extract the version number
            string version = $"{versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}.{versionInfo.FileBuildPart}";

            return version;
        }
    }
}
