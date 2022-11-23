namespace ElizerWork
{
    public class Worker
    {
        private readonly TimeSpan _beatPeriod;
        private readonly List<WorkItem> _workItems = new();
        private readonly object _syncRoot = new();

        public Worker(TimeSpan beatPeriod)
        {
            _beatPeriod = beatPeriod;
        }

        public void Queue(WorkItem workItem)
        {
            lock (_syncRoot)
                _workItems.Add(workItem);
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var activeWorkItems = _workItems.Where(e => e.ExecutionTime <= now).ToArray();
                lock (_syncRoot)
                    foreach (var item in activeWorkItems)
                        _workItems.Remove(item);
                await Task.WhenAll(activeWorkItems.Select(e => e.Execute(cancellationToken)));
                await Task.Delay(_beatPeriod, cancellationToken);
            }
        }
    }
}
