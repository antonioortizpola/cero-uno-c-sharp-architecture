using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceCommon.Invoice.ViewModels
{
    public class InvoiceToAdd
    {
        public string SocialReazon { get; }
        public string Rfc { get; }
        public string Address { get; }
        public DateTime CreatedOn { get; }
        public IList<InvoiceToAddDetail> InvoiceDetails { get; }

        public InvoiceToAdd(
            string socialReazon,
            string rfc,
            string address,
            DateTime createdOn,
            IList<InvoiceToAddDetail> invoiceDetails
            )
        {
            SocialReazon = socialReazon;
            Rfc = rfc;
            Address = address;
            CreatedOn = createdOn;
            InvoiceDetails = invoiceDetails;
        }
    }
}
