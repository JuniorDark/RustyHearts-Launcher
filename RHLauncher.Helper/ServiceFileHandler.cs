using System.Security.Cryptography;

namespace RHLauncher.RHLauncher.Helper
{
    public class ServiceFileHandler
    {
        private readonly string _iniFilePath;
        private readonly string _installDirectory;

        public ServiceFileHandler(string iniFileName, string installDirectory)
        {
            _iniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, iniFileName);
            _installDirectory = installDirectory;
        }

        public void UpdateService()
        {
            string service = new IniFile(_iniFilePath).ReadValue("Info", "Service");
            string md5 = CalculateMD5(service);

            string serviceDatPath = Path.Combine(_installDirectory, "Service.dat");
            string[] lines = File.ReadAllLines(serviceDatPath);

            string currentMd5 = lines[0];
            string currentService = string.Join(Environment.NewLine, lines, 1, lines.Length - 1);

            if (currentMd5 != md5)
            {
                lines[0] = md5;
                File.WriteAllLines(serviceDatPath, lines);
            }
        }

        private static string CalculateMD5(string input)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = MD5.HashData(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
