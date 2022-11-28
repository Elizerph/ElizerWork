namespace ElizerWork
{
    public class WorkItem
    {
        public DateTime NextRun { get; set; }
        public TimeSpan Period { get; }
        public Func<Task> Work { get; }
        public CancellationTokenSource? CancellationSource { get; }

        public WorkItem(DateTime nextRun, TimeSpan period, Func<Task> work, CancellationTokenSource? cancellationSource = null)
        {
            NextRun = nextRun;
            Period = period;
            Work = work ?? throw new ArgumentNullException(nameof(work));
            CancellationSource = cancellationSource;
        }
    }
}
