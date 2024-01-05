using Microsoft.WindowsAPICodePack.Taskbar;
using Newtonsoft.Json;
using RHLauncher.RHLauncher.Helper;
using RHLauncher.RHLauncher.Http;
using RHLauncher.RHLauncher.i8n;
using System.Diagnostics;
using static RHLauncher.RHLauncher.Http.ClientDownloader;
using static RHLauncher.RHLauncher.Http.UpdateDownloader;

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
        public string? installDirectory;
        public string? tempInstallDirectory;
        private static readonly string DefaultIniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.ini");
        private readonly IniFile _iniFile = new(DefaultIniFilePath);
        private readonly string _windyCode;
        private readonly string _password;

        public LauncherForm(string windyCode, string password)
        {
            InitializeComponent();
            _windyCode = windyCode;
            _password = password;
            installDirectory = registryHandler.GetInstallDirectory();
            tempInstallDirectory = registryHandler.GetTempInstallDirectory();

            notifyIcon.Text = LocalizedStrings.RustyHearts;
            Text = LocalizedStrings.LauncherFormTitle;
            LabelNews.Text = LocalizedStrings.LabelNews;
            ChangeInstallLocationButton.Text = LocalizedStrings.InstallLocation;
            UninstallButton.Text = LocalizedStrings.Uninstall;
            OpenSettingsButton.Text = LocalizedStrings.GameSettings;
            CheckUpdateButton.Text = LocalizedStrings.CheckUpdate;
            OpenInstallDirButton.Text = LocalizedStrings.OpenInstallDir;
            ManageButton.Text = LocalizedStrings.Manage;
            ChangePwdButton.Text = LocalizedStrings.ChangePassword;
            LogoutButton.Text = LocalizedStrings.Logout;
            LabelInstalled.Text = LocalizedStrings.AlreadyInstalled;
            LabelLocate.Text = LocalizedStrings.LocateGame;
        }

        #region Form Events
        private async void LauncherForm_Load(object sender, EventArgs e)
        {
            NameLabel.Text = LocalizedStrings.Welcome + ", " + _windyCode;
            ChangeCharPictureBox();
            getUpdatePanel.Visible = false;

            await LoadInstall();
            await WebView2Async();
        }

        private void LauncherForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
            }
        }
        private void LauncherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void LauncherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Icon = null;
            notifyIcon?.Dispose();
        }
        #endregion

        #region Install Methods
        private async Task Install()
        {
            try
            {
                if (string.IsNullOrEmpty(tempInstallDirectory))
                {
                    using FolderBrowserDialog installFbd = new()
                    {
                        Description = LocalizedStrings.SelectInstallLocation,
                        RootFolder = Environment.SpecialFolder.MyComputer,
                        ShowNewFolderButton = true
                    };

                    if (installFbd.ShowDialog() == DialogResult.OK)
                    {
                        tempInstallDirectory = installFbd.SelectedPath;
                        if (!string.IsNullOrEmpty(tempInstallDirectory))
                        {
                            registryHandler.SaveTempInstallDirectory(tempInstallDirectory);
                            await DownloadClient();
                        }
                    }
                }
                else
                {
                    await DownloadClient();
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async Task LoadInstall()
        {
            try
            {
                if (string.IsNullOrEmpty(installDirectory))
                {
                    SetLocateLabels(true);
                    LaunchOptionsButton.Enabled = false;
                    LaunchButton.Text = LocalizedStrings.Install;
                    LaunchButton.Click += InstallButton_Click;
                }
                else
                {
                    DirectoryInfo directoryInfo = new(installDirectory);
                    if (Directory.Exists(installDirectory))
                    {
                        SetLocateLabels(false);
                        ServiceFileHandler updater = new("Config.ini", installDirectory);
                        updater.UpdateService();

                        await CheckForUpdates();
                    }
                    else
                    {
                        registryHandler.ClearInstallDirectory();
                        tempInstallDirectory = null;
                        registryHandler.ClearTempInstallDirectory();
                        LaunchButton.Enabled = true;
                        LaunchButton.Text = LocalizedStrings.Install;
                        LaunchButton.Click -= LaunchGameButton_Click;
                        LaunchButton.Click += InstallButton_Click;
                        LabelInstalled.Visible = true;
                        LabelLocate.Visible = true;
                    }

                }


            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async Task LocateInstall()
        {
            try
            {
                if (string.IsNullOrEmpty(installDirectory))
                {
                    OpenFileDialog ofd = new()
                    {
                        InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                        Filter = "RustyHearts.exe|RustyHearts.exe",
                        Title = LocalizedStrings.SelectExe,
                    };
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        installDirectory = Path.GetDirectoryName(ofd.FileName);
                        if (!string.IsNullOrEmpty(installDirectory))
                        {
                            SetInstallDirectory(installDirectory);
                            await CheckForUpdates();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void SetInstallDirectory(string directory)
        {
            try
            {
                if (!string.IsNullOrEmpty(directory))
                {
                    SetLocateLabels(false);
                    installDirectory = directory;
                    registryHandler.SaveInstallDirectory(directory);
                    tempInstallDirectory = null;
                    registryHandler.ClearTempInstallDirectory();
                    LaunchButton.Click -= InstallButton_Click;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void SetLocateLabels(bool state)
        {
            LabelInstalled.Visible = state;
            LabelLocate.Visible = state;
        }
        #endregion

        #region Update Methods
        private async Task CheckForUpdates()
        {
            try
            {
                LaunchButton.Enabled = false;
                LaunchButton.Text = LocalizedStrings.Checking;
                LaunchOptionsButton.Enabled = false;

                bool updateAvailable = false;
                switch (await CheckForUpdatesAsync())
                {
                    case UpdateState.UpdateAvailable:
                        updateAvailable = true;
                        break;
                    case UpdateState.NoUpdateAvailable:
                    case UpdateState.Error:
                        updateAvailable = false;
                        break;
                }

                SetLaunchButtonHandlers(updateAvailable);
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

        private void SetLaunchButtonHandlers(bool updateAvailable)
        {
            LaunchButton.Click -= InstallButton_Click;
            LaunchButton.Click -= InstallUpdateButton_Click;
            LaunchButton.Click -= LaunchGameButton_Click;

            if (updateAvailable)
            {
                LaunchButton.Text = LocalizedStrings.Update;
                LaunchButton.Click += InstallUpdateButton_Click;
            }
            else
            {
                LaunchButton.Text = LocalizedStrings.Launch;
                LaunchButton.Click += LaunchGameButton_Click;
            }
        }
        #endregion

        #region Download Methods
        private readonly SemaphoreSlim _semaphore = new(1);

        public async Task DownloadClient()
        {
            HidePanels();
            SetLocateLabels(false);
            LaunchButton.Text = LocalizedStrings.Downloading;
            LaunchButton.Enabled = false;
            LaunchOptionsButton.Enabled = false;

            await _semaphore.WaitAsync();
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                Progress<ProgressReport> progress = new(ReportProgress);
                ProgressThrottler progressThrottler = new(progress, 100);

                if (!string.IsNullOrEmpty(tempInstallDirectory))
                {
                    DownloadClientResult result = await DownloadClientAsync(tempInstallDirectory, progressThrottler, _cancellationTokenSource.Token);
                    if (result.Status == DownloadClientResultStatus.Success)
                    {
                        if (result.InstallDirectoryPath != null)
                        {
                            SetInstallDirectory(result.InstallDirectoryPath);
                            await CheckForUpdates();
                        }
                    }
                    else
                    {
                        SetLocateLabels(true);
                        LaunchButton.Text = LocalizedStrings.Install;
                        LaunchButton.Enabled = true;
                        LaunchOptionsButton.Enabled = false;
                    }
                }

            }
            catch (OperationCanceledException ex)
            {
                Logger.WriteLog($"Download canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task InstallUpdateAsync()
        {
            await _semaphore.WaitAsync();
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                Progress<ProgressReport> progress = new(ReportProgress);
                ProgressThrottler progressThrottler = new(progress, 100);

                if (!string.IsNullOrEmpty(installDirectory))
                {
                    await DownloadUpdatesAsync(installDirectory, progressThrottler, _cancellationTokenSource.Token);
                    if (!_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        await Task.Delay(1000);
                        await PatchUpdatesAsync(installDirectory);
                    }
                }

            }
            catch (OperationCanceledException ex)
            {
                Logger.WriteLog($"Update installation canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                HandleException(ex);
                await CheckForUpdates();
            }
            finally
            {
                _semaphore.Release();
                await CheckForUpdates();
            }
        }

        public async Task PatchUpdatesAsync(string installDir)
        {
            try
            {
                if (!string.IsNullOrEmpty(installDir))
                {
                    string mpatcherPath = Path.Combine(installDir, "MPatcher.exe");

                    if (!File.Exists(mpatcherPath))
                    {
                        MsgBoxForm.Show(LocalizedStrings.MPatcher, LocalizedStrings.Error);
                        Logger.WriteLog(LocalizedStrings.MPatcher);
                        return;
                    }

                    ProcessStartInfo startInfo = new()
                    {
                        FileName = mpatcherPath,
                        WorkingDirectory = installDirectory
                    };

                    Process? process = Process.Start(startInfo);

                    if (process != null)
                    {
                        await process.WaitForExitAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private const long ScaleFactor = 100000;
        private void ReportProgress(ProgressReport progressReport)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ProgressReport>(ReportProgress), progressReport);
                return;
            }
            try
            {
                getUpdatePanel.Visible = progressReport.Panel;
                StopButton.Visible = progressReport.CancellButton;
                FileNameLabel.Visible = progressReport.ShowFileNameLabel;
                SpeedLabel.Visible = progressReport.ShowSpeedLabel;
                TimeLabel.Visible = progressReport.ShowTimeLabel;
                FileSizeLabel.Visible = progressReport.ShowFileSizeLabel;
                FileCountLabel.Visible = progressReport.ShowFileCountLabel;
                DownloadingLabel.Text = progressReport.Label;
                PercentLabel.Text = $"{progressReport.PercentComplete}%";
                if (progressReport.IsCheckingFilelist)
                {
                    progressBar.Maximum = (int)progressReport.Total;
                    progressBar.Value = (int)Math.Min(progressReport.Current, progressReport.Total);
                }
                else
                {
                    long scaledMaxValue = progressReport.Total / ScaleFactor;
                    long scaledCurrentValue = progressReport.Current / ScaleFactor;

                    scaledMaxValue = Math.Min(scaledMaxValue, int.MaxValue);
                    scaledCurrentValue = Math.Min(scaledCurrentValue, scaledMaxValue);

                    progressBar.Maximum = (int)scaledMaxValue;
                    progressBar.Value = (int)scaledCurrentValue;
                }

                if (TaskbarManager.IsPlatformSupported)
                {
                    TaskbarManager.Instance.SetProgressValue(progressReport.PercentComplete, 100);
                }

                FileNameLabel.Text = progressReport.FileName;
                SpeedLabel.Text = DownloadHelper.FormatDownloadSpeed(progressReport.TotalSpeed);
                TimeSpan t = TimeSpan.FromSeconds(progressReport.TimeLeft);
                TimeLabel.Text = $"{t.Hours:00}:{t.Minutes:00}:{t.Seconds:00}";
                FileSizeLabel.Text = $"({DownloadHelper.FormatFileSize(progressReport.Current)} / {DownloadHelper.FormatFileSize(progressReport.Total)})";
                FileCountLabel.Text = $"({progressReport.NumFiles}/{progressReport.TotalNumFiles})";
                LaunchButton.Text = progressReport.Button;

                if (!string.IsNullOrEmpty(progressReport.FileName))
                {
                    FileNameLabel.Refresh();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        private static async Task<bool> CheckServerStatusAsync()
        {
            try
            {
                using HttpClient client = new();
                using HttpResponseMessage response = await client.GetAsync(Configuration.Default.GateStatusUrl);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ServerStatusResponse>(json);

                if (result != null && !string.IsNullOrEmpty(result.Status))
                {
                    return (result.Status == "online");
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public class ServerStatusResponse
        {
            public string? Status { get; set; }
        }
        #endregion

        #region Button Click Events

        private async void InstallUpdateButton_Click(object? sender, EventArgs e)
        {
            try
            {
                HidePanels();
                LaunchButton.Text = LocalizedStrings.Updating;
                LaunchButton.Enabled = false;
                LaunchOptionsButton.Enabled = false;
                await InstallUpdateAsync();
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
                await Launch();

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private CancellationTokenSource? _cancellationTokenSource;
        private void StopButton_Click(object sender, EventArgs e)
        {
            try
            {
                _cancellationTokenSource?.Cancel();
                getUpdatePanel.Visible = false;
                LaunchButton.Text = LocalizedStrings.Cancelling;
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void LbLocate_Click(object sender, EventArgs e)
        {
            try
            {
                await LocateInstall();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        private void CharPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeCharPictureBox();
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

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            try
            {
                Logout();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void UpdateCheckButton_Click(object sender, EventArgs e)
        {
            try
            {
                HidePanels();
                await CheckForUpdates();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void InstallButton_Click(object? sender, EventArgs e)
        {
            try
            {
                await Install();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void ChangeInstallLocationButton_Click(object? sender, EventArgs e)
        {
            try
            {
                HidePanels();
                OpenFileDialog ofd = new()
                {
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    Filter = "RustyHearts.exe|RustyHearts.exe",
                    Title = "Select RustyHearts.exe"
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    installDirectory = Path.GetDirectoryName(ofd.FileName);
                    if (!string.IsNullOrEmpty(installDirectory))
                    {
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

        private void ManageButton_Click(object sender, EventArgs e)
        {
            InstallPanel.Visible = !InstallPanel.Visible;
        }

        private void OpenInstallDirButton_Click(object sender, EventArgs e)
        {
            try
            {
                HidePanels();
                if (!string.IsNullOrEmpty(installDirectory))
                {
                    Process.Start("explorer.exe", installDirectory);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void OpenSettingsButton_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            try
            {
                Uninstall();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        private void ChangePwdButton_Click(object sender, EventArgs e)
        {
            try
            {
                HidePanels();
                ChangePwdForm changePwd = new(true);
                changePwd.FormClosing += ChangePwd_FormClosing;
                changePwd.ShowDialog();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void ChangePwd_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (sender is ChangePwdForm changePwd)
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

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            try
            {
                HidePanels();
                ConfigForm configForm = new();
                configForm.ShowDialog();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        #endregion

        #region Button Events

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Restore the form and hide the NotifyIcon when clicked
                Show();
                WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            }
        }

        private void AccOptionsButton_Click(object sender, EventArgs e)
        {
            AccPanel.Visible = !AccPanel.Visible;
        }

        private void Button_MouseHover(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 1;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 0;
            }
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 2;
            }
        }

        private void LabelLocate_MouseHover(object sender, EventArgs e)
        {
            LabelLocate.ForeColor = Color.Gold;
        }

        private void LabelLocate_MouseLeave(object sender, EventArgs e)
        {
            LabelLocate.ForeColor = Color.Goldenrod;
        }

        #endregion

        #region Methods
        private async Task Launch()
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

                if (!string.IsNullOrEmpty(installDirectory))
                {
                    ProcessStartInfo startInfo = new()
                    {
                        FileName = Path.Combine(installDirectory, "RustyHearts.exe"),
                        Arguments = arguments,
                        WorkingDirectory = installDirectory
                    };

                    Process? process = Process.Start(startInfo);

                    if (process != null)
                    {
                        WindowState = FormWindowState.Minimized;

                        if (WindowState == FormWindowState.Minimized)
                        {
                            Hide();
                            notifyIcon.Visible = true;
                        }

                        await process.WaitForExitAsync();

                        Show();
                        WindowState = FormWindowState.Normal;
                        notifyIcon.Visible = false;

                        await CheckForUpdates();
                    }
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Uninstall()
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

                        DirectoryInfo directoryInfo = new(installDirectory);
                        if (Directory.Exists(installDirectory))
                        {
                            foreach (FileInfo file in directoryInfo.GetFiles())
                            {
                                if (file.Name.ToLower() != "launcher.exe" && file.Name.ToLower() != "config.ini")
                                {
                                    file.Delete();
                                }
                            }

                            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
                            {
                                if (!dir.Name.Equals("Launcher.exe.WebView2", StringComparison.OrdinalIgnoreCase))
                                {
                                    dir.Delete(true);
                                }
                            }

                            registryHandler.ClearInstallDirectory();
                            LaunchButton.Enabled = true;
                            LaunchButton.Text = LocalizedStrings.Install;
                            LaunchButton.Click -= InstallButton_Click;
                            LaunchButton.Click -= InstallUpdateButton_Click;
                            LaunchButton.Click -= LaunchGameButton_Click;
                            LaunchButton.Click += InstallButton_Click;
                            LabelInstalled.Visible = true;
                            LabelLocate.Visible = true;
                            MsgBoxForm.Show(LocalizedStrings.UninstallText, LocalizedStrings.Uninstall);

                        }
                        else
                        {
                            registryHandler.ClearInstallDirectory();
                            LaunchButton.Enabled = true;
                            LaunchButton.Text = LocalizedStrings.Install;
                            LaunchButton.Click -= InstallButton_Click;
                            LaunchButton.Click -= InstallUpdateButton_Click;
                            LaunchButton.Click -= LaunchGameButton_Click;
                            LaunchButton.Click += InstallButton_Click;
                            LabelInstalled.Visible = true;
                            LabelLocate.Visible = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                    finally
                    {
                        registryHandler.ClearInstallDirectory();
                        LaunchButton.Enabled = true;
                        LaunchButton.Text = LocalizedStrings.Install;
                        LaunchButton.Click -= InstallButton_Click;
                        LaunchButton.Click -= InstallUpdateButton_Click;
                        LaunchButton.Click -= LaunchGameButton_Click;
                        LaunchButton.Click += InstallButton_Click;
                        LabelInstalled.Visible = true;
                        LabelLocate.Visible = true;
                    }

                }
            }
        }

        public void Logout()
        {
            HidePanels();
            DialogResult result = MsgBoxForm.ShowYN(LocalizedStrings.LogoutText, LocalizedStrings.Confirmation);

            if (result == DialogResult.Yes)
            {
                registryHandler = new RegistryHandler();
                registryHandler.SaveValues(_windyCode, "", false, false);

                LoginForm? loginForm = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
                if (loginForm != null)
                {
                    Invoke((MethodInvoker)(() => Close()));
                    Task.Delay(500).ContinueWith(_ =>
                    {
                        Application.Restart();
                    });
                }
                else
                {
                    LoginForm newLoginForm = new();
                    newLoginForm.Show();
                    Invoke((MethodInvoker)(() => Close()));
                }
            }

        }

        private async Task WebView2Async()
        {
            await webView2.EnsureCoreWebView2Async();
            webView2.Source = new Uri(Configuration.Default.LauncherNewsUrl);
            webView2.CoreWebView2.NavigationStarting += (sender, args) =>
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

        private async void ChangeCharPictureBox()
        {
            await Task.Run(() =>
            {
                Random rnd = new();
                int index = rnd.Next(images.Length);
                Invoke(new Action(() =>
                {
                    CharPictureBox.Image = images[index];
                }));
            });
        }

        private readonly Image[] images = LoadImages();

        private static Image[] LoadImages()
        {
            return new Image[]
            {
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

        private static void HandleException(Exception ex)
        {
            string errorMessage = ex.Message;
            string errorLog = ex.Message + ex.StackTrace;
            Exception newEx = new(errorMessage, ex);
            Exception newLogEx = new(errorLog, ex);
            ExceptionHandler.HandleException(newEx, newLogEx);
        }
        #endregion

    }

}
