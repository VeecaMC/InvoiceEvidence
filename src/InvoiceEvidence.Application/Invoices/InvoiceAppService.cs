using InvoiceEvidence.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace InvoiceEvidence.Invoices
{
    public class InvoiceAppService : ApplicationService, IInvoiceAppService
    {
        private readonly IRepository<Invoice> _repository;

        public InvoiceAppService(IRepository<Invoice> repository)
        {
            _repository = repository;
        }

        [Authorize(InvoiceEvidencePermissions.Invoice.Default)]
        public async Task<PagedResultDto<InvoiceListItemDto>> GetInvoicesListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _repository.GetQueryableAsync();
            var query = queryable
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? nameof(Invoice.IssueDate) : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var invoices = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            return new PagedResultDto<InvoiceListItemDto>(
                totalCount,
                ObjectMapper.Map<List<Invoice>, List<InvoiceListItemDto>>(invoices)
            );
        }

        [Authorize(InvoiceEvidencePermissions.Invoice.Default)]
        public async Task<InvoiceDto> GetInvoiceByIdAsync(Guid id)
        {
            var invoice = await _repository.GetAsync(x => x.InvoiceId == id);
            return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
        }

        [Authorize(InvoiceEvidencePermissions.Invoice.Create)]
        public async Task CreateInvoiceAsync(CreateInvoiceDto createInvoiceDto)
        {
            var invoice = ObjectMapper.Map<CreateInvoiceDto, Invoice>(createInvoiceDto);
            invoice.State = InvoiceState.Created;
            invoice.TotalAmount = 0m;

            await _repository.InsertAsync(invoice, true);
        }

        [Authorize(InvoiceEvidencePermissions.Invoice.Edit)]
        public async Task UpdateInvoiceStateAsync(UpdateInvoiceStateDto updateInvoiceStateDto)
        {
            var invoice = await _repository.GetAsync(x => x.InvoiceId == updateInvoiceStateDto.InvoiceId);

            ObjectMapper.Map<UpdateInvoiceStateDto, Invoice>(updateInvoiceStateDto, invoice);
            await _repository.UpdateAsync(invoice, true);
        }

        public async Task EnsureInvoiceExistsInCreatedStateAsync(Guid invoiceId)
        {
            var invoice =  await _repository.GetAsync(x => x.InvoiceId == invoiceId);
            
            if(invoice.State != InvoiceState.Created)
                throw new AbpValidationException(new List<ValidationResult>()
                {
                    new ValidationResult("The Invoice is not in created state. Cannot edit its InvoiceLines.")
                });
        }

        public async Task RecalculateInvoiceTotalAmount(Guid invoiceId)
        {
            var invoice = await _repository.GetAsync(x => x.InvoiceId == invoiceId);

            if (invoice.InvoiceLines.Count != 0)
                invoice.TotalAmount = invoice.InvoiceLines.Select(x => x.TotalPrice).Sum();
            else
                invoice.TotalAmount = 0m;

            await _repository.UpdateAsync(invoice, true);
        }
    }
}
