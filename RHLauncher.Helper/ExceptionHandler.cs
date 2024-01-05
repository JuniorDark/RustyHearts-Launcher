using RHLauncher.RHLauncher.i8n;

namespace RHLauncher.RHLauncher.Helper
{
    public class ExceptionHandler
    {
        public static void HandleException(Exception ex, Exception exlog)
        {
            string errorMessage = $"{LocalizedStrings.Error}: {ex.Message}";
            string errorLog = $"{LocalizedStrings.Error}: {ex.Message} {Environment.NewLine} {exlog.Message}";
            Logger.WriteLog(errorLog);
            MsgBoxForm.Show(errorMessage, LocalizedStrings.Error);
        }
    }
}
