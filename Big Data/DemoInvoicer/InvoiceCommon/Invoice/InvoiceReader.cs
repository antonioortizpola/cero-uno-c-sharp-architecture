using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceCommon.Invoice.Persistence;
using InvoiceCommon.Invoice.Persistence.ViewModels;
using InvoiceCommon.Invoice.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InvoiceCommon.Invoice
{
    public class InvoiceReader
    {
        private readonly InvoiceContext _context;

        public InvoiceReader(InvoiceContext context)
        {
            _context = context;
        }

        public async Task<InvoiceStatus?> Status(int invoiceId)
        {
            var status = await _context.InvoicesStatus
                .Where(invoiceStatus => invoiceStatus.Id == invoiceId)
                .Select(invoiceStatus => new { invoiceStatus.InvoiceStatus })
                .FirstOrDefaultAsync();
            return status?.InvoiceStatus;
        }

        public async Task<IList<InvoiceReadyForProcess>> FindInvoicesPendingOfProcess()
        {
            var query = from invoiceToProcess in _context.InvoicesToProcess.Include(x => x.InvoiceDetails)
                        join invoiceStatus in _context.InvoicesStatus on invoiceToProcess.Id equals invoiceStatus.Id
                        where invoiceStatus.InvoiceStatus == InvoiceStatus.Created
                        orderby invoiceToProcess.CreatedInSystem
                        select invoiceToProcess;
            return (await query
                    .Take(100)
                    .ToListAsync())
                .Select(ToInvoiceReadyForProcess)
                .ToList();

            //return _context
            //    .InvoicesToProcess.Include(x => x.Details)
            //    .Join(
            //        _context.InvoicesStatus,
            //        invoiceToProcess => invoiceToProcess.Id,
            //        invoiceDetail => invoiceDetail.Id,
            //        (invoiceToProcess, invoiceDetail) => new {invoiceToProcess, invoiceDetail.Status}
            //    )
            //    .Where(x => x.Status == InvoiceStatus.Created)
            //    .Select(x => x.invoiceToProcess)
            //    .Take(100)
            //    .ToList()
            //    .Select(ToInvoiceProcess)
            //    .ToList();
        }

        private static InvoiceReadyForProcess ToInvoiceReadyForProcess(InvoiceToProcessDb invoiceToProcess)
        {
            var invoiceToAdd = new InvoiceToAdd(
                invoiceToProcess.SocialReazon,
                invoiceToProcess.Rfc,
                invoiceToProcess.Address,
                invoiceToProcess.CreatedOn,
                invoiceToProcess.InvoiceDetails
                    .Select(ToInvoiceDetails)
                    .ToList());

            return new InvoiceReadyForProcess(
                invoiceToProcess.Id, invoiceToAdd, invoiceToProcess.CreatedInSystem
            );
        }

        private static InvoiceToAddDetail ToInvoiceDetails(InvoiceToProcessDetailDb invoiceDetailsToProcessDb)
        {
            return new InvoiceToAddDetail(
                invoiceDetailsToProcessDb.Quantity,
                invoiceDetailsToProcessDb.MeasureUnit,
                invoiceDetailsToProcessDb.Description,
                invoiceDetailsToProcessDb.UnitValue);
        }
    }
}
