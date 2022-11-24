namespace ElizerWork
{
    public class WorkerManager
    {
        private readonly Worker _worker;
        private bool _isRunning;
        private Task _run;
        private CancellationTokenSource _runCts;

        public WorkerManager(Worker worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        public void Start() 
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _runCts = new CancellationTokenSource();
                _run = _worker.Run(_runCts.Token);
            }
        }

        public async Task Stop()
        {
            if (_isRunning)
            {
                _isRunning = false;
                _runCts.Cancel();
                await _run;
            }
        }
    }
}
