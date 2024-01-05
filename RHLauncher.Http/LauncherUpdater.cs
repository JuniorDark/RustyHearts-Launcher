using Newtonsoft.Json;
using RHLauncher.RHLauncher.Helper;
using RHLauncher.RHLauncher.i8n;
using System.IO.Compression;
using System.Text;

namespace RHLauncher.RHLauncher.Http
{
    public class LauncherUpdater
    {
        private readonly string LauncherVersionUrl = Configuration.Default.GetLauncherVersion;
        private readonly string LauncherUpdateUrl = Configuration.Default.UpdateLauncherVersion;

        public class LauncherVersion
        {
            public string? Version { get; set; }
        }

        public async Task CheckForLauncherUpdateAsync()
        {
            try
            {
                using HttpClient client = new();
                using HttpResponseMessage response = await client.GetAsync(LauncherVersionUrl);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<LauncherVersion>(json);

                if (result != null)
                {
                    string? newVersion = result.Version;

                    if (!string.IsNullOrEmpty(newVersion))
                    {
                        string currentVersion = GetLauncherVersion.GetVersion();
                        if (!string.IsNullOrEmpty(currentVersion) && !currentVersion.Equals(newVersion, StringComparison.OrdinalIgnoreCase))
                        {
                            string updateMessage = $"{LocalizedStrings.LauncherUpdateText}\n\n" +
                                                   $"{LocalizedStrings.Version}: {newVersion}";

                            MsgBoxForm.Show(updateMessage, LocalizedStrings.Info);
                            await DownloadLauncherUpdateAsync(json);
                        }
                        await Task.Delay(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleLauncherUpdateException(ex);
            }
        }

        private async Task DownloadLauncherUpdateAsync(string version)
        {
            try
            {
                using HttpClient client = new();
                using StringContent content = new(version, Encoding.UTF8, "application/json");

                var result = JsonConvert.DeserializeObject<LauncherVersion>(version);

                if (result != null)
                {
                    string? v = result.Version;

                    if (!string.IsNullOrEmpty(version))
                    {

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

                        MsgBoxForm.Show(LocalizedStrings.LauncherUpdateSuccess + v, LocalizedStrings.Info);
                        await Task.Delay(500).ContinueWith(_ =>
                        {
                            Application.Restart();
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                HandleLauncherUpdateException(ex);
            }
        }

        private static void HandleLauncherUpdateException(Exception ex)
        {
            string errorMessage = LocalizedStrings.LauncherUpdateFailed + ex.Message;
            string errorLog = LocalizedStrings.LauncherUpdateFailed + ex.Message + ex.StackTrace;
            Exception newEx = new(errorMessage, ex);
            Exception newLogEx = new(errorLog, ex);
            ExceptionHandler.HandleException(newEx, newLogEx);
        }
    }
}
