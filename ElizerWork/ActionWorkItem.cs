namespace ElizerWork
{
    public class ActionWorkItem : WorkItem
    {
        private readonly Action _action;

        public ActionWorkItem(DateTime executionTime, Action action)
            : base(executionTime)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override Task Execute(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _action.Invoke();
            return Task.CompletedTask;
        }
    }
}
