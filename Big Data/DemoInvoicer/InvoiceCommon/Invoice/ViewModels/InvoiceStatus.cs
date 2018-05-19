using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceCommon.Invoice.ViewModels
{
    public enum InvoiceStatus
    {
        Created, Processing, Processed, Error, Cancelled
    }

    public static class InvoiceStatusExtensions
    {
        public static string Name(this InvoiceStatus invoiceStatus)
        {
            return Enum.GetName(typeof(InvoiceStatus), invoiceStatus);
        } 
    }
}
