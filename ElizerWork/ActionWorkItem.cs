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
            if (!cancellationToken.IsCancellationRequested)
                _action.Invoke();
            return Task.CompletedTask;
        }
    }
}
