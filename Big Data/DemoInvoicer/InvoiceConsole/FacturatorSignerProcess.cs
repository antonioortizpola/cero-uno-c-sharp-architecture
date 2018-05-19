using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InvoiceConsole.Terminators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InvoiceConsole
{
    public class FacturatorSignerProcess
    {
        private const int MaxItemsInQueue = 50;
        private const int TimeBetweenBatchesInSeconds = 5;
        private const int TransaccionsPerSecond = 2;
        private readonly TerminalCancellationTokenSource _cancellationTokenSource;
        private readonly ConsoleThreadManager _consoleThreadManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FacturatorSignerProcess> _logger;

        public FacturatorSignerProcess(
            TerminalCancellationTokenSource cancellationTokenSource,
            ConsoleThreadManager consoleThreadManager,
            IServiceProvider serviceProvider,
            ILogger<FacturatorSignerProcess> logger)
        {
            _cancellationTokenSource = cancellationTokenSource;
            _consoleThreadManager = consoleThreadManager;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _consoleThreadManager.SetTansactionsPerSecond(20);
        }


        public async Task<int> BeginProcessing()
        {
            try
            {
                await MakeCicle();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "There was an uncontrolled error while processing the batches");
                return 1;
            }
            return 0;
        }

        private async Task MakeCicle()
        {
            while (_cancellationTokenSource.IsValid)
            {
                if (_consoleThreadManager.TotalTasks() > MaxItemsInQueue)
                {
                    _logger.LogInformation("Console thread with {0} items of {1} max. Skip batch process",
                        _consoleThreadManager.TotalTasks(), MaxItemsInQueue);
                }
                else
                {
                    var batchProcessor = _serviceProvider.GetRequiredService<FacturatorSignerProcessBatch>();
                    await batchProcessor.StartBatch();
                }
                if (!_cancellationTokenSource.IsValid) break;
                _logger.LogInformation("Batch processed, waiting for next {0} seconds", TimeBetweenBatchesInSeconds);
                await Task.Delay(TimeSpan.FromSeconds(TimeBetweenBatchesInSeconds));
            }
        }
    }
}
