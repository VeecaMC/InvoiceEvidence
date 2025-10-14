using System.Threading.Tasks;

namespace InvoiceEvidence.Invoices
{
    public interface IInvoiceLineAppService
    {
        public Task<InvoiceLineDto> CreateInvoiceLineAsync(CreateInvoiceLineDto createInvoiceLineDto);

        public Task<InvoiceLineDto> UpdateInvoiceLineAsync(UpdateInvoiceLineDto updateInvoiceLineDto);

        public Task DeleteInvoiceLineAsync(DeleteInvoiceLineDto deleteInvoiceLineDto);
    }
}
