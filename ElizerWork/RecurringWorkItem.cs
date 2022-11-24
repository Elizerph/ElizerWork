namespace ElizerWork
{
    public class RecurringWorkItem : WorkItem
    {
        private readonly Worker _worker;
        private readonly WorkItem _item;
        private readonly TimeSpan _period;

        public RecurringWorkItem(DateTime startTime, Worker worker, WorkItem workItem, TimeSpan period)
            : base(startTime)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
            _item = workItem ?? throw new ArgumentNullException(nameof(workItem));
            _period = period;
        }

        public override async Task Execute(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) 
                return;
            await _item.Execute(cancellationToken);
            var nextTime = StartTime + _period;
            var newWorkItem = new RecurringWorkItem(nextTime, _worker, _item, _period);
            _worker.Queue(newWorkItem);
        }
    }
}
