using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceCommon.Invoice.ViewModels
{
    public class InvoiceToAddDetail
    {
        public decimal Quantity { get; }
        public string MeasureUnit { get; }
        public string Description { get; }
        public decimal UnitValue { get; }

        public InvoiceToAddDetail(
            decimal quantity,
            string measureUnit,
            string description,
            decimal unitValue
            )
        {
            Quantity = quantity;
            MeasureUnit = measureUnit;
            Description = description;
            UnitValue = unitValue;
        }
    }
}
