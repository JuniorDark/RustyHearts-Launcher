using RHLauncher.RHLauncher.Helper;
using RHLauncher.RHLauncher.i8n;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RHLauncher
{
    public partial class RegisterForm : Form
    {
        public string SendCodeUrl = Configuration.Default.SendCodeUrl;
        public string AgreementUrl = Configuration.Default.AgreementUrl;
        public string VerifyCodeUrl = Configuration.Default.VerifyCodeUrl;
        public string RegisterUrl = Configuration.Default.RegisterUrl;
        public string Lang = Configuration.Default.Lang;
        private List<Button>? buttons;
        private List<ImageList>? imageLists;
        private Dictionary<string, List<ImageList>>? languageImageLists;

        private readonly System.Windows.Forms.Timer resendTimer = new();
        private int secondsLeft = 60;

        public RegisterForm()
        {
            InitializeComponent();

            Stage1Panel.Visible = true;
            Stage2Panel.Visible = false;

            LoadLocalizedStrings();

            Text = LocalizedStrings.ChangePwdFormTitle;
            EmailLabel.Text = LocalizedStrings.AccountEmail;
            TitleLabelS1.Text = LocalizedStrings.RegisterAccount;
            SubTitleLabelS1.Text = LocalizedStrings.RustyHearts;
            CodeLabel.Text = LocalizedStrings.VerificationCode;
            TitleLabelS2.Text = LocalizedStrings.AccountDetails;
            SubTitleLabelS2.Text = LocalizedStrings.Account;
            NameLabel.Text = LocalizedStrings.AccountName;
            PasswordLabel.Text = LocalizedStrings.EnterPassword;
            RepeatPasswordLabel.Text = LocalizedStrings.RepeatPassword;
            AgreeCheckBox.Text = LocalizedStrings.AgreeTerms;
            AgreementLabel.Text = LocalizedStrings.UserAgreement;
            NameDescLabel.Text = LocalizedStrings.UsernameDesc;
            PwdDescLabel.Text = LocalizedStrings.NewPasswordDesc;
            PwdConfirmDescLabel.Text = LocalizedStrings.RepeatPasswordDesc;
            ReturnLabelS2.Text = LocalizedStrings.Return;

            resendTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };
            resendTimer.Tick += ResendTimer_Tick;
        }

        private void LoadLocalizedStrings()
        {
            // Initialize buttons and image lists
            buttons = new List<Button> { ContinueButtonS1, SendEmailButton, ContinueButtonS2 };
            imageLists = new List<ImageList> { imageListContinueBtn, imageListSendEmailBtn, imageListContinueBtn };

            // Initialize language-specific image lists
            languageImageLists = new Dictionary<string, List<ImageList>>
        {
            { "en", new List<ImageList> { imageListContinueBtn, imageListSendEmailBtn, imageListContinueBtn } }, // English image lists
            { "ko", new List<ImageList> { imageListContinueBtn_ko, imageListSendEmailBtn_ko, imageListContinueBtn_ko } }, // Korean image lists
            // Add other languages and their respective image lists here
        };

            // Load the appropriate resource file based on the selected language
            LocalizationHelper.LoadLocalizedStrings(Lang, buttons, imageLists, languageImageLists);
        }

        #region Form Events
        private void RegForm_Load(object sender, EventArgs e)
        {
            ContinueButtonS1.Enabled = false;
        }

        private void RegForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            resendTimer.Dispose();
            Dispose();
        }
        #endregion

        #region Methods

        private async Task<string> SendEmailRequestAsync()
        {
            using HttpClient client = new();
            using HttpResponseMessage response = await client.PostAsync(SendCodeUrl, new FormUrlEncodedContent(new[]
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
                case "AccountExists":
                    MsgBoxForm.Show(LocalizedStrings.AccountEmailExists, LocalizedStrings.Info);
                    break;
                case "ValidVerificationCode":
                    // Hide the first panel and show the second panel
                    Stage1Panel.Visible = false;
                    Stage2Panel.Visible = true;
                    EmailLabelS2.Text = EmailTextBox.Text;
                    CodeDescLabel.Text = "";
                    CodePictureBox.Image = imageListTips.Images[1];
                    break;
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
                    MsgBoxForm.Show($"{LocalizedStrings.Error}: {response}", LocalizedStrings.Error);
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
            new KeyValuePair<string, string>("verification_code_type", "Account"),

        }));

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> SendRequestAsync()
        {
            using HttpClient client = new();
            using HttpResponseMessage response = await client.PostAsync(RegisterUrl, new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("windyCode", UsernameTextBox.Text),
            new KeyValuePair<string, string>("email", EmailTextBox.Text),
            new KeyValuePair<string, string>("password", PasswordTextBox.Text)
        }));

            return await response.Content.ReadAsStringAsync();
        }

        private void HandleResponse(string response)
        {
            switch (response)
            {
                case "Success":
                    MsgBoxForm.Show(LocalizedStrings.AccountCreateSuccess, LocalizedStrings.RegisterWindow);
                    Close();
                    break;
                case "AccountExists":
                    MsgBoxForm.Show(LocalizedStrings.AccountExists, LocalizedStrings.Info);
                    break;
                case "WindyCodeExists":
                    MsgBoxForm.Show(LocalizedStrings.WindyCodeExists, LocalizedStrings.Error);
                    break;
                case "InvalidUserNameFormat":
                    MsgBoxForm.Show(LocalizedStrings.InvalidUserNameFormat, LocalizedStrings.Error);
                    break;
                case "InvalidEmailFormat":
                    MsgBoxForm.Show(LocalizedStrings.InvalidEmailFormat, LocalizedStrings.Error);
                    break;
                default:
                    MsgBoxForm.Show($"{LocalizedStrings.Error}: {response}", LocalizedStrings.Error);
                    break;
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

        private void AgreementLabel_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = AgreementUrl, UseShellExecute = true });
        }

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
                Regex emailRegex = RegexPatterns.EmailRegex();

                if (emailRegex.IsMatch(EmailTextBox.Text))
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

        private async void ContinueButtonS2_Click(object sender, EventArgs e)
        {
            ContinueButtonS2.Enabled = false;
            try
            {
                string response = await SendRequestAsync();

                HandleResponse(response);
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
            else if (Stage1Panel.Visible)
            {
                Stage1Panel.Visible = false;
                Stage2Panel.Visible = true;
            }
        }

        private bool NameTextBoxValid = false;
        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            Regex usernameRegex = RegexPatterns.UsernameRegex();

            if (string.IsNullOrEmpty(UsernameTextBox.Text) || UsernameTextBox.Text.Length < 6 || UsernameTextBox.Text.Length > 16 || !usernameRegex.IsMatch(UsernameTextBox.Text) || HasUppercase(UsernameTextBox.Text))
            {
                if (string.IsNullOrEmpty(UsernameTextBox.Text))
                {
                    NameDescLabel.Text = LocalizedStrings.UsernameDescLabelEmpty;
                    NameDescLabel.ForeColor = Color.Red;
                    NamePictureBox.Image = imageListTips.Images[0];
                    NameTextBoxValid = false;
                }
                else if (UsernameTextBox.Text.Length < 6 || UsernameTextBox.Text.Length > 16)
                {
                    NameDescLabel.Text = LocalizedStrings.UsernameDescLabelSize;
                    NameDescLabel.ForeColor = Color.Red;
                    NamePictureBox.Image = imageListTips.Images[0];
                    NameTextBoxValid = false;
                }
                else if (HasUppercase(UsernameTextBox.Text))
                {
                    NameDescLabel.Text = LocalizedStrings.UsernameDescLabelUppercase;
                    NameDescLabel.ForeColor = Color.Red;
                    NamePictureBox.Image = imageListTips.Images[0];
                    NameTextBoxValid = false;
                }
                else if (!usernameRegex.IsMatch(UsernameTextBox.Text))
                {
                    NameDescLabel.Text = LocalizedStrings.UsernameDescLabelInvalid;
                    NameDescLabel.ForeColor = Color.Red;
                    NamePictureBox.Image = imageListTips.Images[0];
                    NameTextBoxValid = false;
                }
            }
            else
            {
                NameDescLabel.Text = "";
                NamePictureBox.Image = imageListTips.Images[1];
                NameTextBoxValid = true;
            }
            CheckFormS2Validity();
        }

        private static bool HasUppercase(string text)
        {
            return text.Any(char.IsUpper);
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
            Regex pwRegex = RegexPatterns.PWRegex();
            if (!pwRegex.IsMatch(password))
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
            Regex strongPWRegex = RegexPatterns.StrongPWRegex();
            if (strongPWRegex.IsMatch(password))
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
                Regex emailRegex = RegexPatterns.EmailRegex();

                if (emailRegex.IsMatch(EmailTextBox.Text))
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
                    CodeDescLabel.Text = LocalizedStrings.CodeDescLabelInvalid;
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

        private void AgreeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckFormS2Validity();
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
            if (NameTextBoxValid &&
                PasswordTextBoxValid &&
                RepeatPasswordTextBoxValid &&
                AgreeCheckBox.Checked)
            {
                ContinueButtonS2.Enabled = true;
            }
            else
            {
                ContinueButtonS2.Enabled = false;
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

        private void Label_MouseHover(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                label.ForeColor = Color.White;
            }
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {

            if (sender is Label label)
            {
                label.ForeColor = Color.Gainsboro;
            }
        }
        #endregion

    }
}
