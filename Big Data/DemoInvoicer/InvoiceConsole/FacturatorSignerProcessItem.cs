using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InvoiceCommon.Invoice;
using InvoiceCommon.Invoice.ViewModels;
using Microsoft.Extensions.Logging;

namespace InvoiceConsole
{
    internal class FacturatorSignerProcessItem
    {
        private readonly InvoiceReader _invoiceReader;
        private readonly InvoiceProcessor _invoiceProcessor;
        private readonly ILogger<FacturatorSignerProcessItem> _logger;

        public FacturatorSignerProcessItem(InvoiceReader invoiceReader, InvoiceProcessor invoiceProcessor, ILogger<FacturatorSignerProcessItem> logger)
        {
            _invoiceReader = invoiceReader;
            _invoiceProcessor = invoiceProcessor;
            _logger = logger;
        }

        public async Task ProcessItem(InvoiceReadyForProcess invoiceReadyForProcess)
        {
            await Task.Delay(1000);
            _logger.LogInformation("Invoice {0} processed", invoiceReadyForProcess.Id);
        }
    }
}
