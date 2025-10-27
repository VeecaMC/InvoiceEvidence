using InvoiceEvidence.Invoices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InvoiceEvidence.Controllers
{
    [Route("invoice")]
    public class InvoiceController : InvoiceEvidenceController
    {
        private readonly IInvoiceAppService _invoiceAppService;

        public InvoiceController(IInvoiceAppService invoiceAppService)
        {
            _invoiceAppService = invoiceAppService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto createInvoiceDto)
        {
            await _invoiceAppService.CreateInvoiceAsync(createInvoiceDto);
            return Created();
        }

        [HttpPatch("set-state")]
        public async Task<IActionResult> UpdateInvoiceState([FromBody] UpdateInvoiceStateDto updateInvoiceStateDto)
        {
            await _invoiceAppService.UpdateInvoiceStateAsync(updateInvoiceStateDto);
            return NoContent();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetInvoiceList([FromQuery] GetInvoiceListDto getInvoiceListDto)
        {
            var result = await _invoiceAppService.GetInvoicesListAsync(getInvoiceListDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(Guid id)
        {
            var result = await _invoiceAppService.GetInvoiceByIdAsync(id);
            return Ok(result);
        }
    }
}
