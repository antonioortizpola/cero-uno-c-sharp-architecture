using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceCommon.Invoice.ViewModels
{
    public class InvoiceReadyForProcess : InvoiceToProcess
    {
        public int Id { get; }

        public InvoiceReadyForProcess(int id, InvoiceToAdd invoiceToAdd, DateTime createdInSystemOn)
            : base(invoiceToAdd, createdInSystemOn)
        {
            Id = id;
        }
    }
}
