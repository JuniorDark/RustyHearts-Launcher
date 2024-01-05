namespace RHLauncher.RHLauncher.Helper
{
    public class ProgressReport
    {
        public string? Label { get; set; }
        public string? Button { get; set; }
        public int PercentComplete { get; set; }
        public string? FileName { get; set; }
        public string? ErrorMessage { get; set; }
        public long Current { get; set; }
        public long Total { get; set; }
        public double TotalSpeed { get; set; }
        public long TimeLeft { get; set; }
        public int NumFiles { get; set; }
        public int TotalNumFiles { get; set; }
        public bool Panel { get; set; }
        public bool CancellButton { get; set; }
        public bool ShowFileNameLabel { get; set; }
        public bool ShowSpeedLabel { get; set; }
        public bool ShowTimeLabel { get; set; }
        public bool ShowFileSizeLabel { get; set; }
        public bool ShowFileCountLabel { get; set; }
        public bool IsCheckingFilelist { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
