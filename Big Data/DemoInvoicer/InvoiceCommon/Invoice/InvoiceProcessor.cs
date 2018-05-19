using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InvoiceCommon.Invoice.Persistence;
using InvoiceCommon.Invoice.Persistence.ViewModels;
using InvoiceCommon.Invoice.ViewModels;

namespace InvoiceCommon.Invoice
{
    public class InvoiceProcessor
    {
        private readonly InvoicePersistence _invoicePersistence;
        private readonly InvoiceStatusManager _invoiceStatusManager;
        private readonly InvoiceJournalRepo _invoiceJournalRepo;
        private readonly DateTimeProvider _dateTimeProvider;

        public InvoiceProcessor(
            InvoicePersistence invoicePersistence,
            InvoiceStatusManager invoiceStatusManager,
            InvoiceJournalRepo invoiceJournalRepo,
            DateTimeProvider dateTimeProvider)
        {
            _invoicePersistence = invoicePersistence;
            _invoiceStatusManager = invoiceStatusManager;
            _invoiceJournalRepo = invoiceJournalRepo;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<int> Incoming(InvoiceToAdd invoiceToAdd)
        {
            var invoiceToProcess = new InvoiceToProcess(invoiceToAdd, _dateTimeProvider.Now());
            var invoiceId = await _invoicePersistence.Incoming(invoiceToProcess);
            await _invoiceStatusManager.Create(invoiceId);
            await _invoiceJournalRepo.Create(invoiceId, "Created");
            return invoiceId;
        }

        public async Task SetIncomingAsProcessing(IList<int> incomingInvoicesToUpdate)
        {
            await _invoicePersistence.SetIncomingAsProcessing(incomingInvoicesToUpdate);

            foreach (var invoiceToUpdate in incomingInvoicesToUpdate)
            {
                await _invoiceJournalRepo.Create(invoiceToUpdate, "Selected by console to process");
            }
        }
    }
}
