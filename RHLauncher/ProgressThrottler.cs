namespace RHLauncher.RHLauncher
{
    public class ProgressThrottler : IProgress<ProgressReport>
    {
        private readonly IProgress<ProgressReport> _progress;
        private readonly int _intervalMs;
        private readonly TaskScheduler _scheduler;

        private readonly object _lock = new();
        private bool _isScheduled = false;
        private ProgressReport? _latestReport;

        public ProgressThrottler(IProgress<ProgressReport> progress, int intervalMs)
        {
            _progress = progress;
            _intervalMs = intervalMs;
            _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        public void Report(ProgressReport value)
        {
            lock (_lock)
            {
                _latestReport = value;
                if (!_isScheduled)
                {
                    _isScheduled = true;
                    Task.Delay(_intervalMs, _latestReport.CancellationToken).ContinueWith(_ =>
                    {
                        lock (_lock)
                        {
                            _progress.Report(_latestReport);
                            _isScheduled = false;
                        }
                    }, _latestReport.CancellationToken, TaskContinuationOptions.ExecuteSynchronously, _scheduler);
                }
            }
        }
    }
}
