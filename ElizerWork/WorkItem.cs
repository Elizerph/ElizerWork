namespace ElizerWork
{
    public abstract class WorkItem
    {
        public DateTime ExecutionTime { get; }

        public WorkItem(DateTime executionTime)
        {
            ExecutionTime = executionTime;
        }

        public abstract Task Execute();
    }
}
