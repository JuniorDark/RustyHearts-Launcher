using RHLauncher.RHLauncher.i8n;
using RHLauncher.RHLauncher.Helper;
using System.Diagnostics;

namespace RHLauncher
{
    public partial class ConfigForm : Form
    {
        public string Url = "https://github.com/JuniorDark/RustyHearts-Launcher";

        private bool languageChanged = false;
        readonly string currentLanguageCode = Configuration.Default.Lang;

        public ConfigForm()
        {
            InitializeComponent();

            string currentVersion = GetLauncherVersion.GetVersion();
            cbLauncherLanguage.SelectedItem = GetLanguageName(currentLanguageCode);

            VersionLabel.Text = $"{LocalizedStrings.Version}: {currentVersion}";
            Text = LocalizedStrings.ConfigFormTitle;
            TitleLabel.Text = LocalizedStrings.Settings;
            LanguageLabel.Text = LocalizedStrings.LauncherLanguage;

        }

        #region Form Events
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            TitleLabel.Left = (ClientSize.Width - TitleLabel.Width) / 2;
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }
        #endregion

        #region Language Methods
        private void CbLauncherLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLauncherLanguage.SelectedItem is string selectedItem)
            {
                string selectedLanguageCode = GetLanguageCode(selectedItem);

                if (selectedLanguageCode != currentLanguageCode)
                {
                    languageChanged = true;
                }
                else
                {
                    languageChanged = false;
                }
            }
        }

        private static string GetLanguageCode(string? language)
        {
            return language switch
            {
                "English" => "en",
                "한국어" => "ko",
                _ => "en", // default to English if not found
            };
        }

        private static string GetLanguageName(string code)
        {
            return code switch
            {
                "en" => "English",
                "ko" => "한국어",
                _ => "English", // default to English if not found
            };
        }
        #endregion

        #region Button Click Events
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (languageChanged)
            {
                string? selectedLanguage = cbLauncherLanguage.SelectedItem.ToString();
                string languageCode = GetLanguageCode(selectedLanguage);

                if (languageCode != null)
                {
                    // Update the language in the INI file
                    Configuration.Default.Lang = languageCode;
                    Configuration.Default.iniFile.WriteValue("Launcher", "Lang", languageCode);

                    DialogResult result = MsgBoxForm.ShowYN(LocalizedStrings.ChangeLanguageMessage, LocalizedStrings.Confirmation);

                    if (result == DialogResult.Yes)
                    {
                        Invoke((MethodInvoker)(() => Close()));
                        Task.Delay(1000).ContinueWith(_ =>
                        {
                            Application.Restart();
                        });
                    }
                }
            }
            else
            {
                Close();
            }
        }

        private void VersionLabel_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = Url, UseShellExecute = true });
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
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
        #endregion
    }
}
