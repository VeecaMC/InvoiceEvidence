using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace InvoiceEvidence.Invoices
{
    public class GetInvoiceListDto : PagedAndSortedResultRequestDto
    {
        [AllowedValues(
            nameof(InvoiceDto.InvoiceId), 
            nameof(InvoiceDto.InvoiceNumber), 
            nameof(InvoiceDto.IssueDate), 
            nameof(InvoiceDto.State), 
            nameof(InvoiceDto.TotalAmount), 
            null)]
        public override string? Sorting { get => base.Sorting; set => base.Sorting = value; }
    }
}
