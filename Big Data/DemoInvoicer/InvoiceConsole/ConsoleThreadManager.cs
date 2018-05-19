using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Logging;

namespace InvoiceConsole
{
    public class ConsoleThreadManager
    {
        private volatile bool _isWorking;

        private double _tickTimeInMilis;
        private decimal _transactionsPerSecond;
        private volatile int _totalTasks;

        private readonly ILogger<ConsoleThreadManager> _logger;
        private readonly object _lockObject;
        private readonly ConcurrentQueue<Func<Task>> _actions;
        private readonly ConcurrentDictionary<Guid, Task> _runningTasks;
        private readonly Timer _timer;

        public ConsoleThreadManager(ILogger<ConsoleThreadManager> logger)
        {
            _logger = logger;
            _actions = new ConcurrentQueue<Func<Task>>();
            _runningTasks = new ConcurrentDictionary<Guid, Task>();
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += OnTimedEvent;
            _lockObject = new object();
            _logger.LogInformation("Console thread manager created and ready");
        }

        public int TotalTasks()
        {
            return _totalTasks;
        }

        public void AddTask(Func<Task> action)
        {
            lock (_lockObject)
            {
                _totalTasks++;
                _actions.Enqueue(action);
                if (!_isWorking)
                {
                    StartThreads();
                }
            }
        }

        private void StartThreads()
        {
            _logger.LogTrace("Starting console thread manager");
            _isWorking = true;
            _timer.AutoReset = true;
            _timer.Start();
            Tick();
        }

        private void StopThreads()
        {
            if (!_isWorking)
            {
                _logger.LogTrace("Console thread manager already stoped");
                return;
            }

            _logger.LogTrace("Stoping console thread manager, no more items");
            _timer.Stop();
            _isWorking = false;
            _timer.AutoReset = true;
        }

        public void SetTansactionsPerSecond(decimal transactionsPerSecond)
        {
            if (transactionsPerSecond == _transactionsPerSecond)
            {
                return;
            }

            lock (_lockObject)
            {
                _transactionsPerSecond = transactionsPerSecond;
                _tickTimeInMilis = (double)(1 / transactionsPerSecond * 1000);
                _timer.Interval = _tickTimeInMilis;
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Tick();
        }

        private void Tick()
        {
            lock (_lockObject)
            {
                _actions.TryDequeue(out var action);

                if (action == null)
                {
                    StopThreads();
                    return;
                }

                _logger.LogTrace("Fire event, {0} tps", _transactionsPerSecond);

                var taskId = Guid.NewGuid();
                var pendingTask = action.Invoke();

                _runningTasks.TryAdd(taskId, pendingTask);

                pendingTask.ContinueWith(_ => TaskCompleted(taskId));
            }
        }

        private void TaskCompleted(Guid taskId)
        {
            lock (_lockObject)
            {
                _runningTasks.TryRemove(taskId, out _);
                _totalTasks--;
            }
        }

        public async Task WaitForAll()
        {
            while (TotalTasks() > 0 && _runningTasks.Count > 0)
            {
                _logger.LogTrace("Waiting for {0} tasks and {1} queued actions to complete", _runningTasks.Count, TotalTasks() - _runningTasks.Count);
                await Task.WhenAll(_runningTasks.Values);
            }
            _logger.LogTrace("All tasks procesed");
        }
    }
}
