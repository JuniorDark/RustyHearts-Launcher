namespace RHLauncher.Helper;

public class Logger
{
    public static void WriteLog(string message)
    {
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Create log file directory if it doesn't exist
        string logFilePath = Path.Combine(appDirectory, "Logs");
        Directory.CreateDirectory(logFilePath);

        // Create log file with today's date
        logFilePath = Path.Combine(logFilePath, "Log-" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt");
        FileInfo logFileInfo = new(logFilePath);
        DirectoryInfo logDirInfo = new(logFileInfo.DirectoryName);
        if (!logDirInfo.Exists)
        {
            logDirInfo.Create();
        }

        // Write log entry to file
        using FileStream fileStream = logFileInfo.Exists ? new FileStream(logFilePath, FileMode.Append) : logFileInfo.Create();
        using StreamWriter streamWriter = new(fileStream);
        streamWriter.WriteLine();
        streamWriter.WriteLine("Log Entry:");
        streamWriter.WriteLine("Date/Time: {0} {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
        streamWriter.WriteLine("Message: {0}", message);
        streamWriter.WriteLine("---------------------------");
    }
}