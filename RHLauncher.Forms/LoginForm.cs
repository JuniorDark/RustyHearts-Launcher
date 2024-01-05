using Newtonsoft.Json;
using RHLauncher.RHLauncher.Helper;
using RHLauncher.RHLauncher.Http;
using RHLauncher.RHLauncher.i8n;
using System.Text.RegularExpressions;

namespace RHLauncher
{
    public partial class LoginForm : Form
    {
        private RegistryHandler registryHandler = new();

        public string windyCode = string.Empty;
        public string password = string.Empty;
        public string LoginUrl = Configuration.Default.LoginUrl;
        public string Lang = Configuration.Default.Lang;
        private List<Button>? buttons;
        private List<ImageList>? imageLists;
        private Dictionary<string, List<ImageList>>? languageImageLists;

        public LoginForm()
        {
            InitializeComponent();

            LoadLocalizedStrings();

            notifyIcon.Text = LocalizedStrings.RustyHearts;
            Text = LocalizedStrings.LauncherFormTitle;
            UsernameLabel.Text = LocalizedStrings.UsernameLabel;
            PasswordLabel.Text = LocalizedStrings.PasswordLabel;
            CheckBoxSaveUser.Text = LocalizedStrings.CheckBoxSaveUser;
            CheckBoxAutoLogin.Text = LocalizedStrings.CheckBoxAutoLogin;
            ForgotPwdLabel.Text = LocalizedStrings.ForgotPwdLabel;
            // Adjust ForgotPwdLabel location for Korean
            if (Lang == "ko")
            {
                ForgotPwdLabel.Location = new Point(630, 410);
            }

        }

        private void LoadLocalizedStrings()
        {
            // Initialize buttons and image lists
            buttons = new List<Button> { LoginButton, RegisterButton };
            imageLists = new List<ImageList> { imageListLogin, imageListRegister };

            // Initialize language-specific image lists
            languageImageLists = new Dictionary<string, List<ImageList>>
        {
            { "en", new List<ImageList> { imageListLogin, imageListRegister } }, // English image lists
            { "ko", new List<ImageList> { imageListLogin_ko, imageListRegister_ko } }, // Korean image lists
            // Add other languages and their respective image lists here
        };

            // Load the appropriate resource file based on the selected language
            LocalizationHelper.LoadLocalizedStrings(Lang, buttons, imageLists, languageImageLists);
        }

        #region Form Events

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            string currentVersion = GetLauncherVersion.GetVersion();
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

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon?.Dispose();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
            }
            else
            {
                notifyIcon.Visible = false;
            }
        }
        #endregion

        #region Login Methods
        private async Task Login()
        {
            Regex usernameRegex = RegexPatterns.UsernameRegex();

            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                MsgBoxForm.Show(LocalizedStrings.LoginInsertUsername, LocalizedStrings.LoginWindowTitle);
                return;
            }
            if (!usernameRegex.IsMatch(UsernameTextBox.Text))
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

                if (loginResponse != null)
                {
                    if (loginResponse.TryGetValue("Result", out var result))
                    {
                        switch (result)
                        {
                            case "LoginSuccess":
                                windyCode = loginResponse["WindyCode"];
                                password = loginResponse["Token"];
                                progressBarLogin.Visible = false;
                                Hide();
                                notifyIcon.Visible = false;
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
                                MsgBoxForm.Show(LocalizedStrings.Error + result, LocalizedStrings.Error);
                                break;
                        }
                    }
                    else
                    {
                        MsgBoxForm.Show(LocalizedStrings.Error + "Response does not contain a Result field.", LocalizedStrings.Error);
                    }
                }
                else
                {
                    MsgBoxForm.Show(LocalizedStrings.Error + "Failed to deserialize the response.", LocalizedStrings.Error);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
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
        #endregion

        #region Methods
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

        private static void HandleException(Exception ex)
        {
            string errorMessage = ex.Message;
            string errorLog = ex.Message + ex.StackTrace;
            Exception newEx = new(errorMessage, ex);
            Exception newLogEx = new(errorLog, ex);
            ExceptionHandler.HandleException(newEx, newLogEx);
        }
        #endregion

        #region Button Click Events
        private async void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                await Login();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                using RegisterForm RegisterForm = new();
                RegisterForm.ShowDialog();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void ForgotPwdLabel_Click(object sender, EventArgs e)
        {
            try
            {
                using ChangePwdForm ChangePwd = new();
                ChangePwd.ShowDialog();
            }
            catch (Exception ex)
            {
                HandleException(ex);
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

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FormUtils.MoveForm(Handle);
            }
        }
        #endregion

        #region Button Events
        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
                WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            }
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
