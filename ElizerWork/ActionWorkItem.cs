namespace ElizerWork
{
    public class ActionWorkItem : WorkItem
    {
        private readonly Action _action;

        public ActionWorkItem(DateTime startTime, Action action)
            : base(startTime)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override Task Execute(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _action.Invoke();
            cancellationToken.ThrowIfCancellationRequested();
            return Task.CompletedTask;
        }
    }
}
