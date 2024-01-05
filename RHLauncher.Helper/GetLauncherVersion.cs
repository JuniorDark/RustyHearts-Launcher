using System.Diagnostics;

namespace RHLauncher.RHLauncher.Helper
{
    public class GetLauncherVersion
    {
        public static string GetVersion()
        {
            // Get the version information of the application
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

            // Extract the version number
            string version = $"{versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}.{versionInfo.FileBuildPart}";

            return version;
        }
    }
}
