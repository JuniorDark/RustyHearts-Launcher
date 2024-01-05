using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;

namespace RHLauncher.RHLauncher.Helper
{
    public class RegistryHandler
    {
        private const string KEY_NAME = "RustyHearts\\UserInfo";
        private const string INSTALL_DIR_KEY = "InstallDirectory";
        private const string TEMP_INSTALL_DIR_KEY = "TempInstallDirectory";
        private readonly RegistryKey key;

        public RegistryHandler()
        {
            key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\" + KEY_NAME, true) ?? Registry.CurrentUser.CreateSubKey("SOFTWARE\\" + KEY_NAME);
            if (!KeyExist())
            {
                CreateKey();
            }
        }

        public void CreateKey()
        {
            string username = key.GetValue("Username")?.ToString() ?? string.Empty;
            string password = key.GetValue("Password")?.ToString() ?? string.Empty;
            string remember = (key.GetValue("Remember") as int?)?.ToString() ?? "0";
            string autoLogin = (key.GetValue("AutoLogin") as int?)?.ToString() ?? "0";

            key.SetValue("Username", username);
            key.SetValue("Password", password);
            key.SetValue("Remember", remember);
            key.SetValue("AutoLogin", autoLogin);
        }

        public bool KeyExist()
        {
            return key.GetValueNames().Length > 0;
        }

        public void SaveValues(string username, string password, bool remember, bool autologin)
        {
            key.SetValue("Username", username ?? string.Empty);
            key.SetValue("Password", password != null ? Encrypt(password) : string.Empty);
            key.SetValue("Remember", remember ? 1 : 0, RegistryValueKind.DWord);
            key.SetValue("AutoLogin", autologin ? 1 : 0, RegistryValueKind.DWord);
        }

        public void SaveUser(string username, bool remember)
        {
            key.SetValue("Username", username ?? string.Empty);
            key.SetValue("Remember", remember ? 1 : 0, RegistryValueKind.DWord);
        }

        public void SaveInstallDirectory(string directory)
        {
            key.SetValue(INSTALL_DIR_KEY, directory ?? string.Empty);
        }

        public void SaveTempInstallDirectory(string directory)
        {
            key.SetValue(TEMP_INSTALL_DIR_KEY, directory ?? string.Empty);
        }

        public string GetInstallDirectory()
        {
            return key.GetValue(INSTALL_DIR_KEY)?.ToString() ?? string.Empty;
        }

        public void ClearInstallDirectory()
        {
            var value = key.GetValue(INSTALL_DIR_KEY)?.ToString();
            if (!string.IsNullOrEmpty(value))
            {
                key.DeleteValue(INSTALL_DIR_KEY);
            }
        }

        public string GetTempInstallDirectory()
        {
            return key.GetValue(TEMP_INSTALL_DIR_KEY)?.ToString() ?? string.Empty;
        }

        public void ClearTempInstallDirectory()
        {
            var value = key.GetValue(TEMP_INSTALL_DIR_KEY)?.ToString();
            if (!string.IsNullOrEmpty(value))
            {
                key.DeleteValue(TEMP_INSTALL_DIR_KEY);
            }
        }

        public void DeleteValues(string KEY_NAME)
        {
            var value = key.GetValue(KEY_NAME) as string ?? "";
            if (!string.IsNullOrEmpty(value))
            {
                key.DeleteValue(KEY_NAME);
            }
        }

        public void ClearPassword()
        {
            key.SetValue("Password", string.Empty);
        }

        public string?[] ReadValues()
        {
            if (KeyExist())
            {
                string?[] values = new string?[4];
                values[0] = (string?)key.GetValue("Username");
                if (!string.IsNullOrEmpty(values[0]))
                {
                    string? encryptedPassword = (string?)key.GetValue("Password");
                    if (!string.IsNullOrEmpty(encryptedPassword))
                    {
                        values[1] = Decrypt(encryptedPassword);
                    }
                    else
                    {
                        values[1] = string.Empty;
                    }
                    int? rememberValue = (int?)key.GetValue("Remember");
                    values[2] = rememberValue?.ToString() ?? "0";
                    int? autoLoginValue = (int?)key.GetValue("AutoLogin");
                    values[3] = autoLoginValue?.ToString() ?? "0";
                    return values;
                }
            }
            return new string?[] { string.Empty, string.Empty, "0", "0" };
        }

        private static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = ProtectedData.Protect(plainTextBytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedBytes);
        }

        private static string Decrypt(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] plainTextBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(plainTextBytes);
        }
    }

}
