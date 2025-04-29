using RHLauncher.RHLauncher.i8n;
using RHLauncher.RHLauncher.Helper;
using System.Diagnostics;
using System.Text;

namespace RHLauncher
{
    public partial class ConfigForm : Form
    {
        private readonly RegistryHandler registryHandler = new();
        private readonly string? installDirectory;

        private readonly string Url = "https://github.com/JuniorDark/RustyHearts-Launcher";

        private bool languageChanged = false;
        readonly string currentLanguageCode = Configuration.Default.Lang;

        private bool serviceChanged = false;
        readonly string currentService = Configuration.Default.Service;

        public ConfigForm()
        {
            InitializeComponent();
            installDirectory = registryHandler.GetInstallDirectory();
            string currentVersion = GetLauncherVersion.GetVersion();
            cbLauncherLanguage.SelectedItem = GetLanguageName(currentLanguageCode);
            cbLauncherService.SelectedItem = GetServiceName(currentService);

            VersionLabel.Text = $"{LocalizedStrings.Version}: {currentVersion}";
            Text = LocalizedStrings.ConfigFormTitle;
            TitleLabel.Text = LocalizedStrings.Settings;
            LanguageLabel.Text = LocalizedStrings.LauncherLanguage;
            ServiceLabel.Text = LocalizedStrings.Service;

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
                _ => "en",
            };
        }

        private static string GetLanguageName(string code)
        {
            return code switch
            {
                "en" => "English",
                "ko" => "한국어",
                _ => "English",
            };
        }
        #endregion

        #region Service Methods

        private void CbLauncherService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLauncherService.SelectedItem is string selectedItem)
            {
                string selectedServiceCode = GetServiceCode(selectedItem);
                if (selectedServiceCode != currentService)
                {
                    serviceChanged = true;
                }
                else
                {
                    serviceChanged = false;
                }
            }
        }

        private static string GetServiceCode(string? service)
        {
            return service switch
            {
                "USA" => "usa",
                "JPN" => "jpn",
                "CHN" => "chn",
                "USA_BETA" => "usa_beta",
                "JPN_BETA" => "jpn_beta",
                "CHN_BETA" => "chn_beta",
                _ => "jpn",
            };
        }

        private static string GetServiceName(string service)
        {
            return service switch
            {
                "usa" => "USA",
                "jpn" => "JPN",
                "chn" => "CHN",
                "usa_beta" => "USA_BETA",
                "jpn_beta" => "JPN_BETA",
                "chn_beta" => "CHN_BETA",
                _ => "JPN",
            };
        }

        private void UpdateServiceDatFile(string serviceCode)
        {
            if (string.IsNullOrEmpty(installDirectory))
                return;

            try
            {
                // Compute MD5 hash
                byte[] inputBytes = Encoding.UTF8.GetBytes(serviceCode);
                byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

                StringBuilder sb = new();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // Lowercase hex
                }
                string hashString = sb.ToString();

                string fileContent = $"{hashString}\r\n\r\nSTAIRWAY GAMES";

                // Write to Service.dat
                string filePath = Path.Combine(installDirectory, "Service.dat");
                File.WriteAllText(filePath, fileContent);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update Service.dat: {ex.Message}", LocalizedStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Button Click Events
        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (languageChanged || serviceChanged)
                {
                    string? selectedService = cbLauncherService.SelectedItem?.ToString();
                    string serviceCode = GetServiceCode(selectedService);
                    if (serviceCode != null)
                    {
                        // Update the service in the INI file
                        Configuration.Default.Service = serviceCode;
                        Configuration.Default.iniFile.WriteValue("Info", "Service", serviceCode);

                        UpdateServiceDatFile(serviceCode);
                    }

                    string? selectedLanguage = cbLauncherLanguage.SelectedItem?.ToString();
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
            catch (Exception ex)
            {
                MessageBox.Show($"{LocalizedStrings.Error}: {ex.Message}", LocalizedStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
