using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using InvoiceCommon.Invoice.ViewModels;

namespace InvoiceCommon.Invoice.Persistence.ViewModels
{
    public class InvoiceStatusManager
    {
        private readonly InvoiceContext _invoiceContext;

        public InvoiceStatusManager(InvoiceContext invoiceContext)
        {
            _invoiceContext = invoiceContext;
        }

        public async Task Create(int invoiceId)
        {
            var invoiceStatus = new InvoiceStatusDb(invoiceId, InvoiceStatus.Created);
            await _invoiceContext.InvoicesStatus
                .AddAsync(invoiceStatus);
            await _invoiceContext.SaveChangesAsync();
        }
    }
}
