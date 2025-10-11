using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceEvidence.Invoices
{
    public interface IInvoiceAppService
    {
        public Task<PagedResultDto<InvoiceListItemDto>?> GetInvoicesListAsync(PagedAndSortedResultRequestDto sortedResultRequestDto);

        public Task<InvoiceDto?> GetInvoiceByIdAsync(Guid id);

        public Task<InvoiceDto?> CreateInvoiceAsync(CreateInvoiceDto createInvoiceDto);

        public Task<InvoiceDto?> UpdateInvoiceStateAsync(UpdateInvoiceStateDto updateInvoiceStateDto);

        public Task EnsureInvoiceExistsInCreatedStateAsync(Guid invoiceId);

        public Task RecalculateInvoiceTotalAmount(Guid invoiceId);
    }
}
