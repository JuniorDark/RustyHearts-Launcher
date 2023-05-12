using RHLauncher.Helper;

namespace RHLauncher.RHLauncher.Helper
{
    public class ExceptionHandler
    {
        public static void HandleException(Exception ex, Exception exlog)
        {
            string errorMessage = $"Error: {ex.Message}";
            string errorLog = $"Error:{ex.Message} {Environment.NewLine} {exlog.Message}";
            Logger.WriteLog(errorLog);
            MsgBoxForm.Show(errorMessage, LocalizedStrings.Error);
        }
    }
}
