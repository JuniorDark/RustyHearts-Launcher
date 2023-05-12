using Newtonsoft.Json;
using RHLauncher.Helper;
using RHLauncher.RHLauncher;
using RHLauncher.RHLauncher.Helper;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RHLauncher
{
    public partial class LoginForm : Form
    {
        private RegistryHandler registryHandler = new();

        public string windyCode = string.Empty;
        public string password = string.Empty;
        public string LoginUrl = Configuration.Default.LoginUrl;

        public LoginForm()
        {
            InitializeComponent();

            UsernameLabel.Text = LocalizedStrings.UsernameLabel;
            PasswordLabel.Text = LocalizedStrings.PasswordLabel;
            CheckBoxSaveUser.Text = LocalizedStrings.CheckBoxSaveUser;
            CheckBoxAutoLogin.Text = LocalizedStrings.CheckBoxAutoLogin;
            ForgotPwdLabel.Text = LocalizedStrings.ForgotPwdLabel;
        }

        #region Methods

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            string currentVersion = GetLauncherVersion();
            VersionLabel.Text = $"{LocalizedStrings.Version}: {currentVersion}";

            registryHandler = new RegistryHandler();
            if (!registryHandler.KeyExist())
            {
                registryHandler.CreateKey();
            }
            else
            {
                string?[] values = registryHandler.ReadValues();
                UsernameTextBox.Text = values[0];
                PasswordTextBox.Text = values[1];
                CheckBoxSaveUser.Checked = values[2] == "1";
                CheckBoxAutoLogin.Checked = values[3] == "1";

                await CheckForLauncherUpdate();

                AutoLogin();
            }

        }

        private static async Task CheckForLauncherUpdate()
        {
            try
            {
                LauncherUpdater launcherUpdater = new();
                await launcherUpdater.CheckForLauncherUpdateAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
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

        private async Task Login()
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                MsgBoxForm.Show(LocalizedStrings.LoginInsertUsername, LocalizedStrings.LoginWindowTitle);
                return;
            }
            if (!Regex.IsMatch(UsernameTextBox.Text, @"^[A-Za-z0-9]{6,50}$|^[\w\d._%+-]+@[\w\d.-]+\.[\w]{2,}$"))
            {
                MsgBoxForm.Show(LocalizedStrings.LoginInvalidUsernameFormat, LocalizedStrings.LoginWindowTitle);
                return;
            }
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MsgBoxForm.Show(LocalizedStrings.LoginInsertPassword, LocalizedStrings.LoginWindowTitle);
                return;
            }

            try
            {
                progressBarLogin.Visible = true;
                progressBarLogin.Style = ProgressBarStyle.Marquee;
                progressBarLogin.MarqueeAnimationSpeed = 30;
                LoginButton.Enabled = false;

                string response = await SendLoginRequestAsync();

                HandleLoginResponse(response);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                progressBarLogin.Visible = false;
                LoginButton.Enabled = true;
            }
        }

        private async void AutoLogin()
        {
            if (CheckBoxAutoLogin.Checked)
            {
                try
                {
                    if (!string.IsNullOrEmpty(UsernameTextBox.Text) && !string.IsNullOrEmpty(PasswordTextBox.Text))
                    {
                        await Login();
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        private async Task<string> SendLoginRequestAsync()
        {
            using HttpClient client = new();
            using HttpResponseMessage response = await client.PostAsync(LoginUrl, new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("account", UsernameTextBox.Text),
            new KeyValuePair<string, string>("password", PasswordTextBox.Text)
        }));

            return await response.Content.ReadAsStringAsync();
        }

        private void HandleLoginResponse(string response)
        {
            try
            {
                Dictionary<string, string>? loginResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

                switch (loginResponse["Result"])
                {
                    case "LoginSuccess":
                        windyCode = loginResponse["WindyCode"];
                        password = loginResponse["Token"];
                        Hide();
                        LauncherForm launcherForm = new(windyCode, password);
                        launcherForm.ShowDialog();
                        break;
                    case "InvalidCredentials":
                        MsgBoxForm.Show(LocalizedStrings.LoginInvalidCredentials, LocalizedStrings.LoginInfoTitle);
                        break;
                    case "InvalidUsernameFormat":
                        MsgBoxForm.Show(LocalizedStrings.LoginInvalidUsernameFormat, LocalizedStrings.LoginInfoTitle);
                        break;
                    case "AccountNotFound":
                        MsgBoxForm.Show(LocalizedStrings.LoginInvalidCredentials, LocalizedStrings.LoginInfoTitle);
                        break;
                    case "Locked":
                        MsgBoxForm.Show(LocalizedStrings.LoginLocked, LocalizedStrings.LoginInfoTitle);
                        break;
                    case "TooManyAttempts":
                        MsgBoxForm.Show(LocalizedStrings.LoginTooManyAttempts, LocalizedStrings.LoginInfoTitle);
                        break;
                    default:
                        MsgBoxForm.Show(LocalizedStrings.Error + loginResponse["Result"], LocalizedStrings.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
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

        private void CheckBoxAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAutoLogin.Checked)
            {
                if (!string.IsNullOrEmpty(UsernameTextBox.Text) && !string.IsNullOrEmpty(PasswordTextBox.Text))
                {
                    registryHandler.SaveValues(UsernameTextBox.Text, PasswordTextBox.Text, true, true);
                }
            }
            else
            {
                registryHandler.SaveValues(UsernameTextBox.Text, "", true, false);
            }
        }

        private void CheckBoxSaveUser_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxSaveUser.Checked)
            {
                if (!string.IsNullOrEmpty(UsernameTextBox.Text))
                {
                    registryHandler.SaveUser(UsernameTextBox.Text, true);
                }
            }
            else
            {
                registryHandler.SaveUser("", false);
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        #endregion

        #region Button Click Events

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            await Login();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FormUtils.MoveForm(Handle);
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            using RegForm RegisterForm = new();
            RegisterForm.ShowDialog();
        }

        private void ForgotPwdLabel_Click(object sender, EventArgs e)
        {
            using ChangePwd ChangePwd = new();
            ChangePwd.ShowDialog();
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region Button Events

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

        private void LoginButton_MouseHover(object sender, EventArgs e)
        {
            LoginButton.ImageIndex = 1;
        }

        private void LoginButton_MouseLeave(object sender, EventArgs e)
        {
            LoginButton.ImageIndex = 0;
        }

        private void LoginButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            LoginButton.ImageIndex = 2;
        }

        private void RegisterButton_MouseHover(object sender, EventArgs e)
        {
            RegisterButton.ImageIndex = 1;
        }

        private void RegisterButton_MouseLeave(object sender, EventArgs e)
        {
            RegisterButton.ImageIndex = 0;
        }
        private void RegisterButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            RegisterButton.ImageIndex = 2;
        }

        private void ForgotPwdLabel_MouseHover(object sender, EventArgs e)
        {
            ForgotPwdLabel.ForeColor = Color.White;
        }

        private void ForgotPwdLabel_MouseLeave(object sender, EventArgs e)
        {
            ForgotPwdLabel.ForeColor = Color.Gainsboro;
        }

        #endregion

    }
}
