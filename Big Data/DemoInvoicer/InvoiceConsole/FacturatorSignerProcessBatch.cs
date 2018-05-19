using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceCommon.Invoice;
using InvoiceCommon.Invoice.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InvoiceConsole
{
    class FacturatorSignerProcessBatch
    {

        private readonly InvoiceReader _invoiceReader;
        private readonly InvoiceProcessor _invoiceProcessor;
        private readonly ConsoleThreadManager _consoleThreadManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FacturatorSignerProcess> _logger;

        public FacturatorSignerProcessBatch(
            InvoiceReader invoiceReader,
            InvoiceProcessor invoiceProcessor,
            ConsoleThreadManager consoleThreadManager,
            IServiceProvider serviceProvider,
            ILogger<FacturatorSignerProcess> logger)
        {
            _invoiceReader = invoiceReader;
            _invoiceProcessor = invoiceProcessor;
            _consoleThreadManager = consoleThreadManager;
            _serviceProvider = serviceProvider;
            _logger = logger;
            logger.LogInformation("Signer processor created");
        }


        public async Task StartBatch()
        {
            var invoicesPending = await _invoiceReader.FindInvoicesPendingOfProcess();
            await _invoiceProcessor.SetIncomingAsProcessing(
                invoicesPending
                    .Select(invoice => invoice.Id)
                    .ToList()
            );
            _logger.LogInformation("Invoices ready to process: {0}", invoicesPending.Count);

            invoicesPending.AsParallel().ForAll(invoice =>
                _consoleThreadManager.AddTask(async () => await ProcessItem(invoice))
            );
        }

        private async Task ProcessItem(InvoiceReadyForProcess invoiceReadyForProcess)
        {
            try
            {
                var facturatorSignerProcessItem =
                    _serviceProvider.GetRequiredService<FacturatorSignerProcessItem>();
                await facturatorSignerProcessItem.ProcessItem(invoiceReadyForProcess);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "There was an error processing the item");
            }
        }
    }
}
