using System.Runtime.InteropServices;
using System.Text;

namespace RHLauncher.RHLauncher.Helper
{
    public partial class IniFile
    {
        private readonly string _iniFilePath;

        [LibraryImport("kernel32", EntryPoint = "WritePrivateProfileStringW", StringMarshalling = StringMarshalling.Utf16)]
        private static partial long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public IniFile(string iniFileName)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _iniFilePath = Path.Combine(appDirectory, iniFileName);

            if (!File.Exists(_iniFilePath))
            {
                //Default api url
                WritePrivateProfileString("Info", "LoginURL", "http://localhost:3000", _iniFilePath);
                //Default client service
                WritePrivateProfileString("Info", "Service", "usa", _iniFilePath);
                //Default launcher language
                WritePrivateProfileString("Launcher", "Lang", "en", _iniFilePath);
            }
        }

        public string ReadValue(string section, string key)
        {
            StringBuilder sb = new(255);
            _ = GetPrivateProfileString(section, key, "", sb, 255, _iniFilePath);
            return sb.ToString();
        }

        public void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _iniFilePath);
        }
    }
}
