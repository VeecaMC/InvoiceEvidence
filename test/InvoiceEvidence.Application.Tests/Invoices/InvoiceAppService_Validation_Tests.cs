using System;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace InvoiceEvidence.Invoices
{
    public abstract class InvoiceAppService_Validation_Tests<TStartupModule> : InvoiceEvidenceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
    {
        private readonly IInvoiceAppService _invoiceAppService;

        protected InvoiceAppService_Validation_Tests()
        {
            _invoiceAppService = GetRequiredService<IInvoiceAppService>();
        }

        [Fact]
        public async Task GetInvoiceListDto_Sorting_Cannot_Be_Invalid()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceAppService.GetInvoicesListAsync(
                new GetInvoiceListDto()
                {
                    SkipCount = 0,
                    MaxResultCount = 10,
                    Sorting = "abc"
                });
            });
        }

        [Fact]
        public async Task UpdateInvoiceStateDto_InvoiceId_Cannot_Be_Empty_Guid()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceAppService.UpdateInvoiceStateAsync(
                new UpdateInvoiceStateDto()
                {
                    InvoiceId = Guid.Empty,
                    State = InvoiceState.Approved
                });
            });
        }

        [Fact]
        public async Task CreateInvoiceDto_InvoiceNumber_Cannot_Be_Zero()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceAppService.CreateInvoiceAsync(
                new CreateInvoiceDto
                {
                    IssueDate = DateTime.UtcNow,
                    InvoiceNumber = 0
                });
            });
        }

        [Fact]
        public async Task CreateInvoiceDto_InvoiceNumber_Cannot_Be_Negative()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceAppService.CreateInvoiceAsync(
                new CreateInvoiceDto
                {
                    IssueDate = DateTime.UtcNow,
                    InvoiceNumber = -10
                });
            });
        }
    }
}
