﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceCommon.Invoice.Persistence.ViewModels;
using InvoiceCommon.Invoice.ViewModels;

namespace InvoiceCommon.Invoice.Persistence
{
    public class InvoicePersistence
    {
        private readonly InvoiceContext _invoiceContext;

        public InvoicePersistence(InvoiceContext invoiceContext)
        {
            _invoiceContext = invoiceContext;
        }

        public async Task<int> Incoming(InvoiceToProcess invoiceToProcess)
        {
            var invoiceToSave = new InvoiceToProcessDb(invoiceToProcess);
            await _invoiceContext.InvoicesToProcess
                .AddAsync(invoiceToSave);
            await _invoiceContext.SaveChangesAsync();
            return invoiceToSave.Id;
        }

        public Task SetIncomingAsProcessing(IList<int> incomingInvoicesToUpdate)
        {
            return _invoiceContext.InvoicesStatus
                .Where(x => incomingInvoicesToUpdate.Contains(x.Id))
                .UpdateFromQueryAsync(x => new InvoiceStatusDb { InvoiceStatus = InvoiceStatus.Processing });
        }
    }
}
