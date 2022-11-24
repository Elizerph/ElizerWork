namespace ElizerWork
{
    public class Worker
    {
        private readonly object _syncRoot = new();
        private readonly TimeSpan _beatPeriod;
        private readonly List<WorkItem> _workItems = new();

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
            try
            {
                while (true)
                {
                    var now = DateTime.Now;
                    var activeWorkItems = _workItems.Where(e => e.StartTime <= now).ToArray();
                    lock (_syncRoot)
                        foreach (var item in activeWorkItems)
                            _workItems.Remove(item);
                    if (activeWorkItems.Any())
                        await Task.WhenAll(activeWorkItems.Select(e => e.Execute(cancellationToken)));
                    await Task.Delay(_beatPeriod, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}
