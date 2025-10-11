using InvoiceEvidence.Invoices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

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
            var result = await _invoiceAppService.CreateInvoiceAsync(createInvoiceDto);
            return Ok(result);
        }

        [HttpPatch("set-state")]
        public async Task<IActionResult> UpdateInvoiceState([FromBody] UpdateInvoiceStateDto updateInvoiceStateDto)
        {
            var result = await _invoiceAppService.UpdateInvoiceStateAsync(updateInvoiceStateDto);
            return Ok(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetInvoiceList([FromQuery] PagedAndSortedResultRequestDto requestDto)
        {
            var result = await _invoiceAppService.GetInvoicesListAsync(requestDto);
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
