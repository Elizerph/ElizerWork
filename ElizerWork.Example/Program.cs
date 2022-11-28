namespace ElizerWork.Example
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start");
            var worker = new Worker(TimeSpan.FromSeconds(1), () => DateTime.Now);

            while (true)
            {
                var key = Console.ReadKey();
                if (key.KeyChar == 's')
                    await worker.Start(CreateWork());
                if (key.KeyChar == 'q')
                    await worker.Stop();
                if (key.KeyChar == 'c')
                    break;
            }

            Console.WriteLine("End");
        }

        private static WorkItem CreateTask(int id)
        { 
            var cts = new CancellationTokenSource();
            var work = async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(1), cts.Token);
                Console.WriteLine($"{DateTime.Now} work {id}");
            };
            return new WorkItem(DateTime.Now, TimeSpan.FromSeconds(5), work, cts);
        }

        private static WorkItem[] CreateWork()
        {
            return new[]
            {
                CreateTask(0),
                CreateTask(1),
                CreateTask(2)
            };
        }
    }
}