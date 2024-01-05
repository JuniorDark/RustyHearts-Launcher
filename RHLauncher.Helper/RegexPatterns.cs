using System.Text.RegularExpressions;

namespace RHLauncher.RHLauncher.Helper
{
    public static partial class RegexPatterns
    {
        [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public static partial Regex EmailRegex();

        [GeneratedRegex("^(?=.*[a-z])[a-z0-9]+$")]
        public static partial Regex UsernameRegex();

        [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).+$")]
        public static partial Regex PWRegex();

        [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).+$")]
        public static partial Regex StrongPWRegex();
    }
}
