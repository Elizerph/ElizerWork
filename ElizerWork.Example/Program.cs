namespace ElizerWork.Example
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start");
            var worker = new Worker(TimeSpan.FromSeconds(1));
            var item = new TaskWorkItem(DateTime.Now, async () => 
            {
                Console.WriteLine("echo work start");
            });
            var recurringItem = new RecurringWorkItem(DateTime.Now, worker, item, TimeSpan.FromSeconds(1));

            worker.Queue(recurringItem);
            var cts = new CancellationTokenSource();
            var prevRun = worker.Run(cts.Token);

            while (true)
            {
                var key = Console.ReadKey();
                if (key.KeyChar == 'q')
                    cts.Cancel();
                if (key.KeyChar == 'r')
                {
                    cts.Cancel();
                    await prevRun;
                    cts = new CancellationTokenSource();
                    worker.Queue(recurringItem);
                    prevRun = worker.Run(cts.Token);
                }
                if (key.KeyChar == 'c')
                    break;
            }

            Console.WriteLine("End");
        }
    }
}