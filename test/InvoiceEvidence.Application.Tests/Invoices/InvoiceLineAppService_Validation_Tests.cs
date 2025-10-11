using System;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace InvoiceEvidence.Invoices
{
    public abstract class InvoiceLineAppService_Validation_Tests<TStartupModule> : InvoiceEvidenceApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IInvoiceAppService _invoiceAppService;
        private readonly IInvoiceLineAppService _invoiceLineAppService;

        protected InvoiceLineAppService_Validation_Tests()
        {
            _invoiceAppService = GetRequiredService<IInvoiceAppService>();
            _invoiceLineAppService = GetRequiredService<IInvoiceLineAppService>();
        }

        [Fact]
        public async Task CreateInvoiceLineDto_InvoiceId_Cannot_Be_Empty_Guid()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.CreateInvoiceLineAsync(
                new CreateInvoiceLineDto()
                {
                    InvoiceId = Guid.Empty,
                    ProductName = "Product",
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task CreateInvoiceLineDto_ProductName_Cannot_Have_More_Characters_Than_128()
        {
            // Arrange
            string productName = string.Empty;
            for(var i = 0; i < 129; i++)
            {
                productName = productName + "A";
            }

            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.CreateInvoiceLineAsync(
                new CreateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductName = productName,
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task CreateInvoiceLineDto_ProductName_Cannot_Be_Empty_String()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.CreateInvoiceLineAsync(
                new CreateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductName = string.Empty,
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task CreateInvoiceLineDto_Quantity_Cannot_Be_Zero()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.CreateInvoiceLineAsync(
                new CreateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductName = "Product",
                    Quantity = 0,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task CreateInvoiceLineDto_Quantity_Cannot_Be_Negative()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.CreateInvoiceLineAsync(
                new CreateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductName = "Product",
                    Quantity = -10,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task UpdateInvoiceLineDto_InvoiceId_And_ProductId_Cannot_Be_The_Same()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(
                new UpdateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductName = "Product",
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task UpdateInvoiceLineDto_ProductId_Cannot_Be_Empty_Guid()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(
                new UpdateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductId = Guid.Empty,
                    ProductName = "Product",
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task UpdateInvoiceLineDto_InvoiceId_Cannot_Be_Empty_Guid()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(
                new UpdateInvoiceLineDto()
                {
                    InvoiceId = Guid.Empty,
                    ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId1,
                    ProductName = "Product",
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task UpdateInvoiceLineDto_ProductName_Cannot_Have_More_Characters_Than_128()
        {
            // Arrange
            string productName = string.Empty;
            for (var i = 0; i < 129; i++)
            {
                productName = productName + "A";
            }

            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(
                new UpdateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId1,
                    ProductName = productName,
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task UpdateInvoiceLineDto_ProductName_Cannot_Be_Empty_String()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(
                new UpdateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId1,
                    ProductName = string.Empty,
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task UpdateInvoiceLineDto_Quantity_Cannot_Be_Zero()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(
                new UpdateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId1,
                    ProductName = "Product",
                    Quantity = 0,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task UpdateInvoiceLineDto_Quantity_Cannot_Be_Negative()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(
                new UpdateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId1,
                    ProductName = "Product",
                    Quantity = -10,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task DeleteInvoiceLineDto_InvoiceId_And_ProductId_Cannot_Be_The_Same()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceLineAppService.DeleteInvoiceLineAsync(
                new DeleteInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceId
                });
            });
        }

        [Fact]
        public async Task DeleteInvoiceLineDto_InvoiceId_Cannot_Be_Empty_Guid()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceLineAppService.DeleteInvoiceLineAsync(
                new DeleteInvoiceLineDto()
                {
                    InvoiceId = Guid.Empty,
                    ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId1
                });
            });
        }

        [Fact]
        public async Task DeleteInvoiceLineDto_ProductId_Cannot_Be_Empty_Guid()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceLineAppService.DeleteInvoiceLineAsync(
                new DeleteInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductId = Guid.Empty
                });
            });
        }
    }
}
