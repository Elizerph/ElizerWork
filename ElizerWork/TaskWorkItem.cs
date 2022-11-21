using System;

namespace ElizerWork
{
    public class TaskWorkItem : WorkItem
    {
        private readonly Func<Task> _task;

        public TaskWorkItem(DateTime executionTime, Func<Task> task)
            : base(executionTime)
        {
            _task = task ?? throw new ArgumentNullException(nameof(task));
        }

        public override Task Execute()
        {
            return _task();
        }
    }
}
