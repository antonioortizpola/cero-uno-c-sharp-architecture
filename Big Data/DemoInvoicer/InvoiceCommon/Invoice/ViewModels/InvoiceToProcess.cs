using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceCommon.Invoice.ViewModels
{
    public class InvoiceToProcess
    {
        public InvoiceToAdd InvoiceToAdd { get; }
        public DateTime CreatedInSystem { get; }

        public InvoiceToProcess(InvoiceToAdd invoiceToAdd, DateTime createdInSystem )
        {
            InvoiceToAdd = invoiceToAdd;
            CreatedInSystem = createdInSystem;
        }
    }
}
