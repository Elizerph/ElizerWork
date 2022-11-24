namespace ElizerWork
{
    public abstract class WorkItem
    {
        public DateTime StartTime { get; }

        public WorkItem(DateTime startTime)
        {
            StartTime = startTime;
        }

        public abstract Task Execute(CancellationToken cancellationToken);
    }
}
