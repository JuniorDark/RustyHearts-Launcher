/*
Rusty Hearts Launcher - Windows Forms Implementation in C#
Author: JuniorDark
GitHub Repository: https://github.com/JuniorDark/RustyHearts-Launcher
This code serves as a starting point for creating your own launcher.
However, it requires further development to improve functionality and
ensure stability. Please check the GitHub repository for updates.
*/

using RHLauncher.RHLauncher.i8n;

namespace RHLauncher
{
    internal static class Program
    {
        private static readonly Mutex mutex = new(false, "RHLauncher");
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new LoginForm());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show(LocalizedStrings.LauncherAlreadyRunning, LocalizedStrings.Error);
            }
        }
    }
}