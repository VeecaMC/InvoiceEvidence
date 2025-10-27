using System.Threading.Tasks;

namespace InvoiceEvidence.Invoices
{
    public interface IInvoiceLineAppService
    {
        public Task CreateInvoiceLineAsync(CreateInvoiceLineDto createInvoiceLineDto);

        public Task UpdateInvoiceLineAsync(UpdateInvoiceLineDto updateInvoiceLineDto);

        public Task DeleteInvoiceLineAsync(DeleteInvoiceLineDto deleteInvoiceLineDto);
    }
}
