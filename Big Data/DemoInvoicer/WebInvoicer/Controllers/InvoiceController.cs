using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InvoiceCommon.Invoice;
using InvoiceCommon.Invoice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebInvoicer.Controllers
{
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly InvoiceProcessor _invoiceProcessor;

        public InvoiceController(IServiceProvider serviceProvider)
        {
            _invoiceProcessor = serviceProvider.GetRequiredService<InvoiceProcessor>();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody][Required]InvoiceToAdd invoiceToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _invoiceProcessor.Incoming(invoiceToAdd);
            return Ok(id);
        }
    }
}
