using InvoiceEvidence.Invoices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoiceEvidence.Controllers
{
    [Route("invoice-line")]
    public class InvoiceLineController : InvoiceEvidenceController
    {
        private readonly IInvoiceLineAppService _invoiceLineAppService;

        public InvoiceLineController(IInvoiceLineAppService invoiceLineAppService)
        {
            _invoiceLineAppService = invoiceLineAppService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoiceLineAsync([FromBody]CreateInvoiceLineDto createInvoiceLineDto)
        {
            var result = await _invoiceLineAppService.CreateInvoiceLineAsync(createInvoiceLineDto);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateInvoiceLineAsync([FromBody] UpdateInvoiceLineDto updateInvoiceLineDto)
        {
            var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(updateInvoiceLineDto);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteInvoiceLineAsync([FromBody] DeleteInvoiceLineDto deleteInvoiceLineDto)
        {
            await _invoiceLineAppService.DeleteInvoiceLineAsync(deleteInvoiceLineDto);
            return NoContent();
        }
    }
}
