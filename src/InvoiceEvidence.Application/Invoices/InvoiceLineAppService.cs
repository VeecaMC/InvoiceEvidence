using InvoiceEvidence.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace InvoiceEvidence.Invoices
{
    public class InvoiceLineAppService : ApplicationService, IInvoiceLineAppService
    {
        private readonly IRepository<InvoiceLine> _repository;
        private readonly IInvoiceAppService _invoiceAppService;
        
        public InvoiceLineAppService(IRepository<InvoiceLine> repository, IInvoiceAppService invoiceAppService)
        {
            _repository = repository;
            _invoiceAppService = invoiceAppService;
        }

        [Authorize(InvoiceEvidencePermissions.Invoice.Edit)]
        public async Task<InvoiceLineDto> CreateInvoiceLineAsync(CreateInvoiceLineDto createInvoiceLineDto)
        {
            await _invoiceAppService.EnsureInvoiceExistsInCreatedStateAsync(createInvoiceLineDto.InvoiceId);

            var invoiceLine = ObjectMapper.Map<CreateInvoiceLineDto, InvoiceLine>(createInvoiceLineDto);
            CalculateTotalPrice(invoiceLine);

            await _repository.InsertAsync(invoiceLine, true);

            await _invoiceAppService.RecalculateInvoiceTotalAmount(createInvoiceLineDto.InvoiceId);

            return ObjectMapper.Map<InvoiceLine, InvoiceLineDto>(invoiceLine);
        }

        [Authorize(InvoiceEvidencePermissions.Invoice.Edit)]
        public async Task<InvoiceLineDto> UpdateInvoiceLineAsync(UpdateInvoiceLineDto updateInvoiceLineDto)
        {
            await _invoiceAppService.EnsureInvoiceExistsInCreatedStateAsync(updateInvoiceLineDto.InvoiceId);

            var invoiceLine = await _repository.GetAsync(x => x.ProductId == updateInvoiceLineDto.ProductId);

            ObjectMapper.Map(updateInvoiceLineDto, invoiceLine);
            CalculateTotalPrice(invoiceLine);

            await _repository.UpdateAsync(invoiceLine, true);

            await _invoiceAppService.RecalculateInvoiceTotalAmount(updateInvoiceLineDto.InvoiceId);

            return ObjectMapper.Map<InvoiceLine, InvoiceLineDto>(invoiceLine);
        }

        [Authorize(InvoiceEvidencePermissions.Invoice.Edit)]
        public async Task DeleteInvoiceLineAsync(DeleteInvoiceLineDto deleteInvoiceLineDto)
        {
            await _invoiceAppService.EnsureInvoiceExistsInCreatedStateAsync(deleteInvoiceLineDto.InvoiceId);

            await _repository.DeleteAsync(x => x.ProductId == deleteInvoiceLineDto.ProductId, true);

            await _invoiceAppService.RecalculateInvoiceTotalAmount(deleteInvoiceLineDto.InvoiceId);
        }

        private static void CalculateTotalPrice(InvoiceLine invoiceLine)
        {
            invoiceLine.TotalPrice = invoiceLine.UnitPrice * invoiceLine.Quantity;
        }
    }
}
