using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using InvoiceCommon.Invoice.ViewModels;

namespace InvoiceCommon.Invoice.Persistence.ViewModels
{
    public class InvoiceStatusDb
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }

        private InvoiceStatusDb()
        {
        }

        public InvoiceStatusDb(int id, InvoiceStatus invoiceStatus)
        {
            Id = id;
            InvoiceStatus = invoiceStatus;
        }
    }
}
