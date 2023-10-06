using Newtonsoft.Json;
using RHLauncher.Helper;
using RHLauncher.RHLauncher;
using RHLauncher.RHLauncher.Helper;
using System.Diagnostics;
using static RHLauncher.RHLauncher.UpdateDownloader;
using static RHLauncher.RHLauncher.UpdateInstaller;
using static RHLauncher.RHLauncher.UpdateUnpacker;

/*
Rusty Hearts Launcher - Windows Forms Implementation in C#
Author: JuniorDark
GitHub Repository: https://github.com/JuniorDark/RustyHearts-Launcher
This code serves as a starting point for creating your own launcher.
However, it requires further development to improve functionality and
ensure stability. Please check the GitHub repository for updates.
*/


namespace RHLauncher
{
    public partial class LauncherForm : Form
    {
        private RegistryHandler registryHandler = new();
        private readonly CancellationTokenSource? cancellationTokenSource;
        public string? installDirectory;
        private static readonly string DefaultIniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
        private readonly IniFile _iniFile = new(DefaultIniFilePath);
        private readonly string _windyCode;
        private readonly string _password;
        private readonly Image[] images = {
            Properties.Resources.character_select_cut_angela,
            Properties.Resources.character_select_cut_edgar,
            Properties.Resources.character_select_cut_frantz,
            Properties.Resources.character_select_cut_ian,
            Properties.Resources.character_select_cut_leila,
            Properties.Resources.character_select_cut_meilinchen,
            Properties.Resources.character_select_cut_natasha,
            Properties.Resources.character_select_cut_roselle,
            Properties.Resources.character_select_cut_tude
        };

        public LauncherForm(string windyCode, string password)
        {
            InitializeComponent();

            LabelNews.Text = LocalizedStrings.LabelNews;

            _windyCode = windyCode;
            _password = password;
            installDirectory = registryHandler.GetInstallDirectory();
        }

        #region Methods

        private async void LauncherForm_Load(object sender, EventArgs e)
        {
            NameLabel.Text = LocalizedStrings.Welcome + ", " + _windyCode;
            ChangeCharPictureBox();
            getUpdatePanel.Hide();

            if (string.IsNullOrEmpty(installDirectory))
            {
                LaunchOptionsButton.Enabled = false;
                LaunchButton.Text = LocalizedStrings.Install;
                LaunchButton.Click += (s, e) =>
                {
                    Install();
                };
            }
            else
            {
                var updater = new ServiceFileHandler("config.ini", installDirectory);
                updater.UpdateService();

                await CheckForUpdates();
            }

            await WebView2Async();
        }

        private async Task WebView2Async()
        {
            await webView21.EnsureCoreWebView2Async();
            webView21.Source = new Uri(Configuration.Default.LauncherNewsUrl);
            webView21.CoreWebView2.NavigationStarting += (sender, args) =>
            {
                // Check if the URL being navigated to is an external link
                if (!args.Uri.ToString().StartsWith(Configuration.Default.LauncherNewsUrl))
                {
                    // Cancel the navigation
                    args.Cancel = true;

                    // Launch the URL in the user's default browser
                    Process.Start(new ProcessStartInfo(args.Uri) { UseShellExecute = true });
                }
            };
        }

        private async void Install()
        {
            try
            {
                if (string.IsNullOrEmpty(installDirectory))
                {
                    OpenFileDialog openFileDialog1 = new()
                    {
                        InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                        Filter = "RustyHearts.exe|RustyHearts.exe",
                        Title = "Select RustyHearts.exe"
                    };
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        installDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
                        registryHandler.SaveInstallDirectory(installDirectory);
                        await CheckForUpdates();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async Task CheckForUpdates()
        {
            try
            {
                LaunchButton.Enabled = false;
                LaunchButton.Text = LocalizedStrings.Checking;
                LaunchOptionsButton.Enabled = false;

                switch (await CheckForUpdatesAsync())
                {
                    case UpdateState.UpdateAvailable:
                        LaunchButton.Text = LocalizedStrings.Update;
                        LaunchButton.Enabled = true;
                        LaunchButton.Click -= LaunchGameButton_Click;
                        LaunchButton.Click += InstallUpdateButton_Click;

                        break;
                    case UpdateState.NoUpdateAvailable:
                        LaunchButton.Text = LocalizedStrings.Launch;
                        LaunchButton.Click -= LaunchGameButton_Click;
                        LaunchButton.Click += LaunchGameButton_Click;
                        break;
                    case UpdateState.Error:
                        LaunchButton.Text = LocalizedStrings.Launch;
                        LaunchButton.Click -= LaunchGameButton_Click;
                        LaunchButton.Click += LaunchGameButton_Click;
                        break;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                LaunchButton.Enabled = true;
                LaunchOptionsButton.Enabled = true;
                AccOptionsButton.Enabled = true;
            }
        }

        private readonly SemaphoreSlim _semaphore = new(1);

        public async Task InstallUpdate()
        {
            await _semaphore.WaitAsync();
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                Progress<ProgressReport> progress = new(ReportProgress);
                ProgressThrottler progressThrottler = new(progress, 200);

                Progress<ProgressReport> packProgress = new(ReportInstallProgress);
                ProgressThrottler packProgressThrottler = new(packProgress, 200);

                await DownloadUpdatesAsync(installDirectory, progressThrottler, _cancellationTokenSource.Token);
                await UnpackDownloadedFilesAsync(installDirectory, packProgressThrottler, _cancellationTokenSource.Token);
                await PackDownloadedFilesAsync(installDirectory, packProgressThrottler, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException ex)
            {
                Logger.WriteLog($"Update installation canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                _semaphore.Release();
                LaunchButton.Enabled = true;
                LaunchOptionsButton.Enabled = true;
                LaunchButton.Text = LocalizedStrings.Launch;
                LaunchButton.Click -= InstallUpdateButton_Click;
                LaunchButton.Click += LaunchGameButton_Click;

                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    await CheckForUpdates();
                }
            }

        }

        private static void HandleException(Exception ex)
        {
            string errorMessage = ex.Message;
            string errorLog = ex.Message + ex.StackTrace;
            Exception newEx = new(errorMessage, ex);
            Exception newLogEx = new(errorLog, ex);
            ExceptionHandler.HandleException(newEx, newLogEx);
        }

        private void ReportProgress(ProgressReport progressReport)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ProgressReport>(ReportProgress), progressReport);
                return;
            }
            FileNameLabel.Visible = progressReport.ShowFileNameLabel;
            SpeedLabel.Visible = progressReport.ShowSpeedLabel;
            TimeLabel.Visible = progressReport.ShowTimeLabel;
            FileSizeLabel.Visible = progressReport.ShowFileSizeLabel;
            FileCountLabel.Visible = progressReport.ShowFileCountLabel;
            DownloadingLabel.Text = progressReport.Label;
            PercentLabel.Text = $"{progressReport.PercentComplete}%";
            progressBar1.Maximum = (int)progressReport.TotalBytesToDownload;
            progressBar1.Value = (int)Math.Min(progressReport.BytesDownloaded, progressReport.TotalBytesToDownload);
            FileNameLabel.Text = progressReport.FileName;
            SpeedLabel.Text = FormatDownloadSpeed(progressReport.TotalSpeed);
            TimeSpan t = TimeSpan.FromSeconds(progressReport.TimeLeft);
            TimeLabel.Text = $"{t.Hours:00}:{t.Minutes:00}:{t.Seconds:00}";
            FileSizeLabel.Text = $"({FormatFileSize(progressReport.BytesDownloaded)} / {FormatFileSize(progressReport.TotalBytesToDownload)})";
            FileCountLabel.Text = $"({progressReport.NumFilesDownloaded}/{progressReport.TotalNumFiles})";

            if (!string.IsNullOrEmpty(progressReport.FileName))
            {
                FileNameLabel.Refresh();
            }

            getUpdatePanel.Visible = progressReport.Panel;
        }

        private void ReportInstallProgress(ProgressReport progressReport)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ProgressReport>(ReportInstallProgress), progressReport);
                return;
            }
            FileNameLabel.Visible = progressReport.ShowFileNameLabel;
            SpeedLabel.Visible = progressReport.ShowSpeedLabel;
            TimeLabel.Visible = progressReport.ShowTimeLabel;
            FileSizeLabel.Visible = progressReport.ShowFileSizeLabel;
            FileCountLabel.Visible = progressReport.ShowFileCountLabel;
            DownloadingLabel.Text = progressReport.Label;
            PercentLabel.Text = $"{progressReport.PercentComplete}%";
            progressBar1.Maximum = progressReport.TotalNumFiles;
            progressBar1.Value = progressReport.NumFilesPacked;
            FileNameLabel.Text = progressReport.FileName;
            FileCountLabel.Text = $"({progressReport.NumFilesPacked}/{progressReport.TotalNumFiles})";

            if (!string.IsNullOrEmpty(progressReport.FileName))
            {
                FileNameLabel.Refresh();
            }

            getUpdatePanel.Visible = progressReport.Panel;
        }

        public void ChangeCharPictureBox()
        {
            Random rnd = new();
            int index = rnd.Next(images.Length);
            CharPictureBox.Image = images[index];
        }

        private static async Task<bool> CheckServerStatusAsync()
        {
            try
            {
                using HttpClient client = new();
                using HttpResponseMessage response = await client.GetAsync(Configuration.Default.GateStatusUrl);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(json);
                string status = result.status;
                return (status == "online");
            }
            catch
            {
                return false;
            }
        }

        private void CharPictureBox_Click(object sender, EventArgs e)
        {
            ChangeCharPictureBox();
        }

        private static void HidePanel(Panel panel)
        {
            if (panel.Visible)
            {
                panel.Visible = false;
            }
        }

        private void HidePanels()
        {
            HidePanel(AccPanel);
            HidePanel(LaunchPanel);
            HidePanel(InstallPanel);
        }

        #endregion

        #region Button Click Events

        private CancellationTokenSource? _cancellationTokenSource;
        private void StopButton_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            getUpdatePanel.Visible = false;
            LaunchButton.Text = LocalizedStrings.Cancelling;
            LaunchButton.Click -= InstallUpdateButton_Click;
        }

        private async void InstallUpdateButton_Click(object? sender, EventArgs e)
        {
            try
            {
                LaunchButton.Text = LocalizedStrings.Updating;
                LaunchButton.Enabled = false;
                LaunchOptionsButton.Enabled = false;
                await InstallUpdate();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void LaunchGameButton_Click(object? sender, EventArgs e)
        {
            try
            {
                HidePanels();
                LaunchButton.Text = LocalizedStrings.Launching;
                LaunchButton.Enabled = false;
                LaunchOptionsButton.Enabled = false;
                AccOptionsButton.Enabled = false;

                // Check if RustyHearts.exe is already running
                if (Process.GetProcessesByName("RustyHearts").Length > 0)
                {
                    MsgBoxForm.Show(LocalizedStrings.AlreadyExecute, LocalizedStrings.Error);
                    await CheckForUpdates();
                    return;
                }

                // Check if server is online
                bool serverOnline = await CheckServerStatusAsync();
                if (!serverOnline)
                {
                    MsgBoxForm.Show(LocalizedStrings.ServerOffline, LocalizedStrings.Error);
                    await CheckForUpdates();
                    return;
                }

                LaunchButton.Text = LocalizedStrings.Running;
                LaunchButton.Enabled = false;
                LaunchOptionsButton.Enabled = false;
                AccOptionsButton.Enabled = false;

                string service = _iniFile.ReadValue("Info", "Service");
                string arguments = string.Empty;

                switch (service.ToLower())
                {
                    case "usa":
                        arguments = "server=" + Configuration.Default.GateXMLUrl;
                        break;
                    case "chn":
                        arguments = $"-serverurl{Configuration.Default.GateInfoUrl} id={_windyCode} password={_password}";
                        break;
                    default:
                        // handle unsupported service
                        MsgBoxForm.Show(LocalizedStrings.UnsupportedService, LocalizedStrings.Error);
                        await CheckForUpdates();
                        return;
                }

                ProcessStartInfo startInfo = new()
                {
                    FileName = Path.Combine(installDirectory, "RustyHearts.exe"),
                    Arguments = arguments,
                    WorkingDirectory = installDirectory
                };
                Process? process = Process.Start(startInfo);
                WindowState = FormWindowState.Minimized;
                await process.WaitForExitAsync();
                WindowState = FormWindowState.Maximized;
                await CheckForUpdates();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            HidePanels();

            if (e.Button == MouseButtons.Left)
            {
                FormUtils.MoveForm(Handle);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void LauncherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            HidePanels();
            DialogResult result = MsgBoxForm.ShowYN(LocalizedStrings.LogoutText, LocalizedStrings.Confirmation);

            if (result == DialogResult.Yes)
            {
                registryHandler = new RegistryHandler();
                registryHandler.SaveValues(_windyCode, "", false, false);
                Logout();
            }

        }

        public void Logout()
        {
            LoginForm loginForm = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
            if (loginForm != null)
            {
                // Close the current form before restarting
                Close();

                // Restart the application
                Application.Restart();
            }
            else
            {
                LoginForm newLoginForm = new LoginForm();
                newLoginForm.Show();
                Close();
            }
        }


        private async void UpdateCheckButton_Click(object sender, EventArgs e)
        {
            HidePanels();
            await CheckForUpdates();
        }

        private async void InstallButton_Click(object? sender, EventArgs e)
        {
            HidePanels();
            OpenFileDialog openFileDialog1 = new()
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = "RustyHearts.exe|RustyHearts.exe",
                Title = "Select RustyHearts.exe"
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                installDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
                registryHandler.SaveInstallDirectory(installDirectory);
                await CheckForUpdates();
            }
        }

        private void ManageButton_Click(object sender, EventArgs e)
        {
            InstallPanel.Visible = !InstallPanel.Visible;
        }

        private void OpenInstallDirButton_Click(object sender, EventArgs e)
        {
            HidePanels();
            if (!string.IsNullOrEmpty(installDirectory))
            {
                Process.Start("explorer.exe", installDirectory);
            }
        }

        private void OpenSettingsButton_Click(object sender, EventArgs e)
        {
            HidePanels();
            if (!string.IsNullOrEmpty(installDirectory))
            {
                string rustyHeartsConfigPath = Path.Combine(installDirectory, "rustyheartsconfig.exe");
                if (File.Exists(rustyHeartsConfigPath))
                {
                    ProcessStartInfo startInfo = new()
                    {
                        FileName = rustyHeartsConfigPath,
                        WorkingDirectory = installDirectory

                    };
                    Process.Start(startInfo);
                }
                else
                {
                    MsgBoxForm.Show(LocalizedStrings.rustyheartsconfig, LocalizedStrings.Error);
                }
            }
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            HidePanels();
            if (!string.IsNullOrEmpty(installDirectory))
            {
                DialogResult result = MsgBoxForm.ShowYN(LocalizedStrings.ConfirmUninstallText, LocalizedStrings.ConfirmUninstall);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        LaunchButton.Enabled = false;
                        LaunchOptionsButton.Enabled = false;
                        LaunchButton.Text = LocalizedStrings.Uninstalling;
                        Directory.Delete(installDirectory, true);
                        MsgBoxForm.Show(LocalizedStrings.UninstallText, LocalizedStrings.Uninstall);
                        registryHandler.ClearInstallDirectory();
                        LaunchButton.Enabled = true;
                        LaunchButton.Text = LocalizedStrings.Install;
                        LaunchButton.Click -= LaunchGameButton_Click;
                        LaunchButton.Click += InstallButton_Click;

                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }
            }
        }

        private void ChangePwdButton_Click(object sender, EventArgs e)
        {
            HidePanels();
            ChangePwd changePwd = new(true);
            changePwd.FormClosing += ChangePwd_FormClosing;
            changePwd.ShowDialog();
        }

        private async void ChangePwd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sender is ChangePwd changePwd)
            {
                changePwd.Dispose();
            }
        }

        private void LaunchOptionsButton_Click(object sender, EventArgs e)
        {
            LaunchPanel.Visible = !LaunchPanel.Visible;
            if (InstallPanel.Visible)
            {
                InstallPanel.Visible = false;
            }
        }

        #endregion

        #region Button Events

        private void MenuButton_MouseHover(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.ImageIndex = 1;
        }

        private void MenuButton_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.ImageIndex = 0;
        }

        private void MenuButton_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.ImageIndex = 2;
        }

        private void AccOptionsButton_Click(object sender, EventArgs e)
        {
            AccPanel.Visible = !AccPanel.Visible;
        }

        private void MinimizeButton_MouseHover(object sender, EventArgs e)
        {
            MinimizeButton.ImageIndex = 1;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ImageIndex = 0;
        }

        private void MinimizeButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            MinimizeButton.ImageIndex = 2;
        }

        private void CloseButton_MouseHover(object sender, EventArgs e)
        {
            CloseButton.ImageIndex = 1;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ImageIndex = 0;
        }
        private void CloseButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            CloseButton.ImageIndex = 2;
        }

        private void LaunchButton_MouseHover(object sender, EventArgs e)
        {
            LaunchButton.ImageIndex = 1;
        }
        private void LaunchButton_MouseLeave(object sender, EventArgs e)
        {
            LaunchButton.ImageIndex = 0;
        }
        private void LaunchButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            LaunchButton.ImageIndex = 2;
        }

        private void AccOptionsButton_MouseHover(object sender, EventArgs e)
        {
            AccOptionsButton.ImageIndex = 1;
        }

        private void AccOptionsButton_MouseLeave(object sender, EventArgs e)
        {
            AccOptionsButton.ImageIndex = 0;
        }

        private void LaunchOptionsButton_MouseHover(object sender, EventArgs e)
        {
            LaunchOptionsButton.ImageIndex = 1;
        }
        private void LaunchOptionsButton_MouseLeave(object sender, EventArgs e)
        {
            LaunchOptionsButton.ImageIndex = 0;
        }
        private void LaunchOptionsButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            LaunchOptionsButton.ImageIndex = 2;
        }

        private void StopButton_MouseHover(object sender, EventArgs e)
        {
            StopButton.ImageIndex = 1;
        }
        private void StopButton_MouseLeave(object sender, EventArgs e)
        {
            StopButton.ImageIndex = 0;
        }
        private void StopButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            StopButton.ImageIndex = 2;
        }
        #endregion
    }

}
