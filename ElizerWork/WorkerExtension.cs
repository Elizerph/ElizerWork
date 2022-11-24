using System.Runtime.CompilerServices;

namespace ElizerWork
{
    public static class WorkerExtension
    {
        public static void Queue(this Worker worker, DateTime startTime, Action action)
        {
            var workItem = new ActionWorkItem(startTime, action);
            worker.Queue(workItem);
        }

        public static void Queue(this Worker worker, DateTime startTime, Func<Task> task) 
        {
            var workItem = new TaskWorkItem(startTime, task);
            worker.Queue(workItem);
        }

        public static void QueueRecurring(this Worker worker, DateTime startTime, TimeSpan period, Action action)
        {
            var workItem = new ActionWorkItem(startTime, action);
            var recurringWorkItem = new RecurringWorkItem(startTime, worker, workItem, period);
            worker.Queue(recurringWorkItem);
        }

        public static void QueueRecurring(this Worker worker, DateTime startTime, TimeSpan period, Func<Task> task)
        {
            var workItem = new TaskWorkItem(startTime, task);
            var recurringWorkItem = new RecurringWorkItem(startTime, worker, workItem, period);
            worker.Queue(recurringWorkItem);
        }

        public static WorkerManager GetManager(this Worker worker)
        {
            return new WorkerManager(worker);
        }
    }
}
