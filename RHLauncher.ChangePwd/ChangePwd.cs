using RHLauncher.Helper;
using RHLauncher.RHLauncher;
using RHLauncher.RHLauncher.Helper;
using System.Text.RegularExpressions;

namespace RHLauncher
{
    public partial class ChangePwd : Form
    {
        private RegistryHandler registryHandler = new();

        public string SendPasswordCodeUrl = Configuration.Default.SendPasswordCodeUrl;
        public string VerifyCodeUrl = Configuration.Default.VerifyCodeUrl;
        public string ChangePasswordUrl = Configuration.Default.ChangePasswordUrl;

        private readonly System.Windows.Forms.Timer resendTimer = new();
        private int secondsLeft = 60;

        private readonly bool _shouldRestart;

        public ChangePwd(bool shouldRestart = false)
        {
            InitializeComponent();

            Stage1Panel.Visible = true;
            Stage2Panel.Visible = false;

            resendTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };
            resendTimer.Tick += ResendTimer_Tick;
            _shouldRestart = shouldRestart;


            TitleLabelS1.Text = LocalizedStrings.ChangePassword;
            TitleLabelS2.Text = LocalizedStrings.ChangePassword;
            SubTitleLabelS1.Text = LocalizedStrings.RustyHearts;
            SubTitleLabelS2.Text = LocalizedStrings.Account;
            DescLabelS1.Text = LocalizedStrings.EnterEmail;
            ReturnLabelS2.Text = LocalizedStrings.Return;
            CodeLabel.Text = LocalizedStrings.VerificationCode;
            PasswordLabel.Text = LocalizedStrings.NewPassword;
            PwdDescLabel.Text = LocalizedStrings.NewPasswordDesc;
            RepeatPasswordLabel.Text = LocalizedStrings.RepeatPassword;
            PwdConfirmDescLabel.Text = LocalizedStrings.RepeatPasswordDesc;

        }

        #region Methods

        private async Task<string> SendEmailRequestAsync()
        {
            using HttpClient client = new();
            using HttpResponseMessage response = await client.PostAsync(SendPasswordCodeUrl, new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("email", EmailTextBox.Text),

        }));

            return await response.Content.ReadAsStringAsync();
        }

        private void HandleSendEmailResponse(string response)
        {
            switch (response)
            {
                case "EmailSent":
                    SendEmailButton.Enabled = false;
                    resendTimer.Start();
                    break;
                case "ValidVerificationCode":
                    // Hide the firs panel and show the second panel
                    Stage1Panel.Visible = false;
                    Stage2Panel.Visible = true;
                    EmailLabelS2.Text = EmailTextBox.Text;
                    CodeDescLabel.Text = "";
                    CodePictureBox.Image = imageListTips.Images[1];
                    break;
                case "PasswordChanged":
                    MsgBoxForm.Show(LocalizedStrings.PasswordChanged, LocalizedStrings.Success);
                    OnPasswordChanged();
                    break;
                case "SamePassword":
                    MsgBoxForm.Show(LocalizedStrings.SamePassword, LocalizedStrings.Failed);
                    break;
                case "AccountNotFound":
                    EmailDescLabel.Text = LocalizedStrings.AccountNotFound;
                    EmailDescLabel.ForeColor = Color.Red;
                    EmailPictureBox.Image = imageListTips.Images[0];
                    return;
                case "InvalidVerificationCode":
                    CodeDescLabel.Text = LocalizedStrings.InvalidVerificationCode;
                    CodeDescLabel.ForeColor = Color.Red;
                    CodePictureBox.Image = imageListTips.Images[0];
                    return;
                case "ExpiredVerificationCode":
                    CodeDescLabel.Text = LocalizedStrings.ExpiredVerificationCode;
                    CodeDescLabel.ForeColor = Color.Red;
                    CodePictureBox.Image = imageListTips.Images[0];
                    return;
                default:
                    MsgBoxForm.Show("Error:" + response, LocalizedStrings.Error);
                    break;
            }
        }

        private void ResendTimer_Tick(object? sender, EventArgs e)
        {
            // Decrement the secondsLeft variable and update the button text
            secondsLeft--;
            TimerLabel.Text = $"({secondsLeft})";

            // If the timer has finished counting down, stop the timer and enable the ResendEmailButton
            if (secondsLeft == 0)
            {
                resendTimer.Stop();
                SendEmailButton.Enabled = true;
                TimerLabel.Text = "";
            }
        }

        private async Task<string> VerifyCodeSendRequestAsync()
        {
            using HttpClient client = new();
            using HttpResponseMessage response = await client.PostAsync(VerifyCodeUrl, new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("email", EmailTextBox.Text),
            new KeyValuePair<string, string>("verification_code", CodeTextBox.Text),
            new KeyValuePair<string, string>("verification_code_type", "Password"),

        }));

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> ChangePasswordSendRequestAsync()
        {
            using HttpClient client = new();
            using HttpResponseMessage response = await client.PostAsync(ChangePasswordUrl, new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("email", EmailTextBox.Text),
            new KeyValuePair<string, string>("password", PasswordTextBox.Text),
            new KeyValuePair<string, string>("verification_code", CodeTextBox.Text),

        }));

            return await response.Content.ReadAsStringAsync();
        }

        private void OnPasswordChanged()
        {
            if (_shouldRestart)
            {
                registryHandler = new RegistryHandler();
                registryHandler.ClearPassword();

                Application.Restart();
            }
            else
            {
                Close();
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

        private async void SendEmailButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                EmailDescLabel.Text = LocalizedStrings.EmailDescLabelEmpty;
                EmailDescLabel.ForeColor = Color.Red;
                EmailPictureBox.Image = imageListTips.Images[0];
                return;
            }

            // Disable the ResendEmailButton and stop the timer
            SendEmailButton.Enabled = false;
            resendTimer.Stop();

            try
            {
                string email = EmailTextBox.Text;
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (Regex.IsMatch(email, pattern))
                {
                    // email is valid
                    EmailPictureBox.Image = imageListTips.Images[1];
                    EmailDescLabel.Text = "";
                }
                else
                {
                    // email is not valid
                    EmailPictureBox.Image = imageListTips.Images[0];
                    EmailDescLabel.Text = LocalizedStrings.EmailDescLabelInvalid;
                    return;
                }

                // Reset the secondsLeft variable and start the timer again
                secondsLeft = 60;
                resendTimer.Start();
                SendEmailButton.Enabled = false;

                string response = await SendEmailRequestAsync();

                HandleSendEmailResponse(response);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void ContinueButtonS1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodeTextBox.Text))
            {
                CodeDescLabel.Text = LocalizedStrings.CodeDescLabel;
                CodeDescLabel.ForeColor = Color.Red;
                CodePictureBox.Image = imageListTips.Images[0];
                return;
            }

            try
            {
                string response = await VerifyCodeSendRequestAsync();

                HandleSendEmailResponse(response);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        private async void OkButtonS2_Click(object sender, EventArgs e)
        {
            try
            {
                string response = await ChangePasswordSendRequestAsync();

                HandleSendEmailResponse(response);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        private void ReturnLabel_Click(object sender, EventArgs e)
        {
            // Show the previous panel and hide the current panel
            if (Stage2Panel.Visible)
            {
                Stage1Panel.Visible = true;
                Stage2Panel.Visible = false;
            }
        }

        private bool PasswordTextBoxValid = false;
        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            string password = PasswordTextBox.Text;

            // Check for minimum length and maximum length
            if (password.Length < 6 || password.Length > 16)
            {
                PwdDescLabel.Text = LocalizedStrings.PwdDescLabelSize;
                PwdDescLabel.ForeColor = Color.Red;
                PwdPictureBox.Image = imageListTips.Images[0];
                PasswordTextBoxValid = false;
                RepeatPasswordTextBoxValid = false;
                CheckFormS2Validity();
                return;
            }

            // Check for at least one uppercase, one lowercase letter, and one number
            Regex regex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$");
            if (!regex.IsMatch(password))
            {
                PwdDescLabel.Text = LocalizedStrings.PwdDescLabelCriteria;
                PwdDescLabel.ForeColor = Color.Red;
                PwdPictureBox.Image = imageListTips.Images[0];
                PwdStrengthLabel.Text = LocalizedStrings.PwdStrengthLabelWeak;
                PwdStrengthLabel.ForeColor = Color.Red;
                PasswordTextBoxValid = false;
                RepeatPasswordTextBoxValid = false;
                CheckFormS2Validity();
                return;
            }

            // Check for additional character types such as symbols
            regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).+$");
            if (regex.IsMatch(password))
            {
                PwdDescLabel.Text = "";
                PwdPictureBox.Image = imageListTips.Images[1];
                PwdStrengthLabel.Text = LocalizedStrings.PwdStrengthLabelStrong;
                PwdStrengthLabel.ForeColor = Color.Green;
                PasswordTextBoxValid = true;
                RepeatPasswordTextBox_TextChanged(sender, e);
                return;
            }

            // Password is valid but could be stronger
            PwdDescLabel.Text = "";
            PwdPictureBox.Image = imageListTips.Images[1];
            PwdStrengthLabel.Text = LocalizedStrings.PwdStrengthLabelMedium;
            PwdStrengthLabel.ForeColor = Color.Yellow;
            PasswordTextBoxValid = true;
            RepeatPasswordTextBox_TextChanged(sender, e);
            return;
        }

        private bool RepeatPasswordTextBoxValid = false;
        private void RepeatPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(RepeatPasswordTextBox.Text))
            {
                PwdConfirmDescLabel.Text = LocalizedStrings.PwdConfirmDescLabelEmpty;
                PwdConfirmDescLabel.ForeColor = Color.Red;
                PwdConfirmPictureBox.Image = imageListTips.Images[0];
                RepeatPasswordTextBoxValid = RepeatPasswordTextBox.Text.Equals(PasswordTextBox.Text);
            }
            else if (!RepeatPasswordTextBox.Text.Equals(PasswordTextBox.Text))
            {
                PwdConfirmDescLabel.Text = LocalizedStrings.PwdConfirmDescLabelMatch;
                PwdConfirmDescLabel.ForeColor = Color.Red;
                PwdConfirmPictureBox.Image = imageListTips.Images[0];
                RepeatPasswordTextBoxValid = RepeatPasswordTextBox.Text.Equals(PasswordTextBox.Text);
            }
            else
            {
                PwdConfirmDescLabel.Text = "";
                PwdConfirmPictureBox.Image = imageListTips.Images[1];
                RepeatPasswordTextBoxValid = RepeatPasswordTextBox.Text.Equals(PasswordTextBox.Text);
            }
            CheckFormS2Validity();
        }


        private bool EmailTextBoxValid = false;
        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                EmailDescLabel.Text = LocalizedStrings.EmailDescLabelEmpty;
                EmailDescLabel.ForeColor = Color.Red;
                EmailPictureBox.Image = imageListTips.Images[0];
                EmailTextBoxValid = false;
            }
            try
            {
                string email = EmailTextBox.Text;
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (Regex.IsMatch(email, pattern))
                {
                    // email is valid
                    EmailPictureBox.Image = imageListTips.Images[1];
                    EmailDescLabel.Text = "";
                    EmailTextBoxValid = true;
                }
                else
                {
                    // email is not valid
                    EmailPictureBox.Image = imageListTips.Images[0];
                    EmailDescLabel.Text = LocalizedStrings.EmailDescLabelInvalid;
                    EmailTextBoxValid = false;
                }

            }
            catch (FormatException)
            {
                EmailPictureBox.Image = imageListTips.Images[0];
            }
            CheckFormS1Validity();
        }

        private bool CodeTextBoxValid = false;
        private void CodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodeTextBox.Text))
            {
                CodeDescLabel.Text = LocalizedStrings.CodeDescLabel;
                CodeDescLabel.ForeColor = Color.Red;
                CodePictureBox.Image = imageListTips.Images[0];
                CodeTextBoxValid = false;
            }
            try
            {
                string input = CodeTextBox.Text;
                if (int.TryParse(input, out int number))
                {
                    // input is numeric
                    CodeDescLabel.Text = "";
                    CodePictureBox.Image = imageListTips.Images[1];
                    CodeTextBoxValid = true;
                }
                else
                {
                    // input is not numeric
                    CodeDescLabel.Text = LocalizedStrings.InvalidVerificationCode;
                    CodeDescLabel.ForeColor = Color.Red;
                    CodePictureBox.Image = imageListTips.Images[0];
                    CodeTextBoxValid = false;
                }
            }
            catch (FormatException)
            {
                CodePictureBox.Image = imageListTips.Images[0];
            }
            CheckFormS1Validity();
        }

        private void CheckFormS1Validity()
        {
            if (EmailTextBoxValid &&
                CodeTextBoxValid)
            {
                ContinueButtonS1.Enabled = true;
            }
            else
            {
                ContinueButtonS1.Enabled = false;
            }
        }

        private void CheckFormS2Validity()
        {
            if (PasswordTextBoxValid &&
                RepeatPasswordTextBoxValid)
            {
                OkButtonS2.Enabled = true;
            }
            else
            {
                OkButtonS2.Enabled = false;
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

        #endregion

        #region Button Events

        private void ContinueButtonS1_MouseHover(object sender, EventArgs e)
        {
            ContinueButtonS1.ImageIndex = 1;
        }

        private void ContinueButtonS1_MouseLeave(object sender, EventArgs e)
        {
            ContinueButtonS1.ImageIndex = 0;
        }
        private void ContinueButtonS1_OnMouseDown(object sender, MouseEventArgs e)
        {
            ContinueButtonS1.ImageIndex = 2;
        }

        private void OkButtonS2_MouseHover(object sender, EventArgs e)
        {
            OkButtonS2.ImageIndex = 1;
        }

        private void OkButtonS2_MouseLeave(object sender, EventArgs e)
        {
            OkButtonS2.ImageIndex = 0;
        }
        private void OkButtonS2_OnMouseDown(object sender, MouseEventArgs e)
        {
            OkButtonS2.ImageIndex = 2;
        }

        private void SendEmailButton_MouseHover(object sender, EventArgs e)
        {
            SendEmailButton.ImageIndex = 1;
        }

        private void SendEmailButton_MouseLeave(object sender, EventArgs e)
        {
            SendEmailButton.ImageIndex = 0;
        }
        private void SendEmailButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            SendEmailButton.ImageIndex = 2;
        }

        private void ChangePwd_FormClosing(object sender, FormClosingEventArgs e)
        {
            resendTimer.Dispose();
            if (sender is ChangePwd changePwd)
            {
                changePwd.Dispose();
            }
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

        private void ReturnLabelS2_MouseHover(object sender, EventArgs e)
        {
            ReturnLabelS2.ForeColor = Color.White;
        }

        private void ReturnLabelS2_MouseLeave(object sender, EventArgs e)
        {
            ReturnLabelS2.ForeColor = Color.Gainsboro;
        }

        private void ChangePwd_Load(object sender, EventArgs e)
        {
            ContinueButtonS1.Enabled = false;
        }

        #endregion
    }
}
