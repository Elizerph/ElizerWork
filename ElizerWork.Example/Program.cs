namespace ElizerWork.Example
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start");
            var worker = new Worker(TimeSpan.FromSeconds(1));
            worker.QueueRecurring(DateTime.Now, TimeSpan.FromSeconds(10), async () => Console.WriteLine("work"));
            var manager = worker.GetManager();
            manager.Start();

            while (true)
            {
                var key = Console.ReadKey();
                if (key.KeyChar == 'q')
                    await manager.Stop();
                if (key.KeyChar == 'r')
                {
                    await manager.Stop();
                    manager.Start();
                }
                if (key.KeyChar == 'c')
                    break;
            }

            Console.WriteLine("End");
        }
    }
}