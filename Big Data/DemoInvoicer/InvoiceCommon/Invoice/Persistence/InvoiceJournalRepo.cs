using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InvoiceCommon.Invoice.ViewModels;

namespace InvoiceCommon.Invoice.Persistence
{
    public class InvoiceJournalRepo
    {
        private readonly InvoiceContext _invoiceContext;
        private readonly DateTimeProvider _dateTimeProvider;

        public InvoiceJournalRepo(InvoiceContext invoiceContext, DateTimeProvider dateTimeProvider)
        {
            _invoiceContext = invoiceContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task Create(int invoiceId, string log)
        {
            var invoiceJournal = new InvoiceJournalDb(invoiceId, log, _dateTimeProvider.Now());
            await _invoiceContext.InvoicesJournal
                .AddAsync(invoiceJournal);
            await _invoiceContext.SaveChangesAsync();
        }
    }
}
