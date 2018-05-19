using System;
using System.Collections.Generic;
using System.Text;
using InvoiceCommon.Invoice.ViewModels;

namespace InvoiceCommon.Invoice.Persistence.ViewModels
{
    public class InvoiceToProcessDetailDb
    {
        public static InvoiceToProcessDetailDb FromInvoiceDetail(InvoiceToAddDetail invoiceToAddDetail)
        {
            return new InvoiceToProcessDetailDb()
            {
                Quantity = invoiceToAddDetail.Quantity,
                MeasureUnit = invoiceToAddDetail.MeasureUnit,
                Description = invoiceToAddDetail.Description,
                UnitValue = invoiceToAddDetail.UnitValue
            };
        }

        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string MeasureUnit { get; set; }
        public string Description { get; set; }
        public decimal UnitValue { get; set; }
    }
}
