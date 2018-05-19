using System;
using System.Collections.Generic;
using System.Text;
using InvoiceCommon.Invoice.Persistence.ViewModels;
using InvoiceCommon.Invoice.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InvoiceCommon.Invoice.Persistence
{
    public class InvoiceContext: DbContext
    {
        public InvoiceContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<InvoiceToProcessDb> InvoicesToProcess { get; set; }
        public DbSet<InvoiceToProcessDetailDb> InvoicesDetailsToProcess { get; set; }
        public DbSet<InvoiceStatusDb> InvoicesStatus { get; set; }
        public DbSet<InvoiceJournalDb> InvoicesJournal { get; set; }
    }
}
