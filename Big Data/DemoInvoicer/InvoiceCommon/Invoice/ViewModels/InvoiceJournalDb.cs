using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceCommon.Invoice.ViewModels
{
    public class InvoiceJournalDb
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Log { get; set; }
        public DateTime ChangedOn { get; set; }

        public InvoiceJournalDb(int invoiceId, string log, DateTime changedOn)
        {
            InvoiceId = invoiceId;
            Log = log;
            ChangedOn = changedOn;
        }
    }
}
