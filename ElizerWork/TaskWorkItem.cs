using System;

namespace ElizerWork
{
    public class TaskWorkItem : WorkItem
    {
        private readonly Func<Task> _task;

        public TaskWorkItem(DateTime startTime, Func<Task> task)
            : base(startTime)
        {
            _task = task ?? throw new ArgumentNullException(nameof(task));
        }

        public override async Task Execute(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _task();
            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
