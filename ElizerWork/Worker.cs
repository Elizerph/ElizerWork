namespace ElizerWork
{
    public class Worker
    {
        private readonly TimeSpan _beat;
        private readonly Func<DateTime> _getNow;
        private IReadOnlyCollection<WorkItem>? _workItems;
        private bool _isRunning;
        private Task? _run;
        private CancellationTokenSource? _runCts;

        public Worker(TimeSpan beat, Func<DateTime> getNow)
        { 
            _beat = beat;
            _getNow = getNow;
        }

        public async Task Start(IReadOnlyCollection<WorkItem> workItems)
        {
            await Stop();
            _workItems = workItems ?? throw new ArgumentNullException(nameof(workItems));
            _isRunning = true;
            _runCts = new CancellationTokenSource();
            _run = Run(_runCts.Token);
        }

        private async Task Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested) 
            {
                var now = _getNow();
                if (_workItems != null)
                    foreach (var itemToRun in _workItems.Where(e => e.NextRun <= now))
                    {
                        var periodsFromLastRun = (now.Ticks - itemToRun.NextRun.Ticks) / itemToRun.Period.Ticks;
                        var nextRunTicks = itemToRun.NextRun.Ticks + (periodsFromLastRun + 1) * itemToRun.Period.Ticks;
                        itemToRun.NextRun = new DateTime(nextRunTicks);
                        _ = itemToRun.Work();
                    }
                await Task.Delay(_beat, cancellationToken);
            }
        }

        public async Task Stop() 
        {
            if (_isRunning)
            {
                _isRunning = false;
                if (_workItems != null)
                    foreach (var item in _workItems)
                        item.CancellationSource?.Cancel();
                _runCts?.Cancel();
                try
                {
                    if (_run != null)
                        await _run;
                }
                catch (OperationCanceledException)
                {
                }
            }
        }
    }
}
