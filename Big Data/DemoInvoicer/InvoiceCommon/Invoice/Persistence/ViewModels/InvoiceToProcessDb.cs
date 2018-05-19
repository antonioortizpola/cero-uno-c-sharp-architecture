using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InvoiceCommon.Invoice.ViewModels;

namespace InvoiceCommon.Invoice.Persistence.ViewModels
{
    public class InvoiceToProcessDb
    {
        private InvoiceToProcessDb()
        {
        }

        public InvoiceToProcessDb(InvoiceToProcess invoiceToProcess)
        {
            var invoiceToAdd = invoiceToProcess.InvoiceToAdd;
            SocialReazon = invoiceToAdd.SocialReazon;
            Rfc = invoiceToAdd.Rfc;
            Address = invoiceToAdd.Address;
            CreatedOn = invoiceToAdd.CreatedOn;
            CreatedInSystem = invoiceToProcess.CreatedInSystem;
            InvoiceDetails = invoiceToAdd.InvoiceDetails
                .Select(InvoiceToProcessDetailDb.FromInvoiceDetail)
                .ToList();
        }

        public int Id { get; set; }
        public string SocialReazon { get; set; }
        public string Rfc { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime CreatedInSystem { get; set; }
        public IList<InvoiceToProcessDetailDb> InvoiceDetails { get; set; }
    }
}
