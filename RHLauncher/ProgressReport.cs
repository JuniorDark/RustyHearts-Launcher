namespace RHLauncher.RHLauncher
{
    public class ProgressReport
    {
        public string? Label { get; set; }
        public int PercentComplete { get; set; }
        public string? FileName { get; set; }
        public string? ErrorMessage { get; set; }
        public long BytesDownloaded { get; set; }
        public long TotalBytesToDownload { get; set; }
        public double TotalSpeed { get; set; }
        public long TimeLeft { get; set; }
        public int NumFilesDownloaded { get; set; }
        public int TotalNumFiles { get; set; }
        public int NumFilesPacked { get; set; }
        public int NumFilesUnpacked { get; set; }
        public bool Panel { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public bool ShowFileNameLabel { get; set; }
        public bool ShowSpeedLabel { get; set; }
        public bool ShowTimeLabel { get; set; }
        public bool ShowFileSizeLabel { get; set; }
        public bool ShowFileCountLabel { get; set; }
    }
}
