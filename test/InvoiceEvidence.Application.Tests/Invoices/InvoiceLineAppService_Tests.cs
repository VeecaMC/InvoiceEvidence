using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace InvoiceEvidence.Invoices
{
    public abstract class InvoiceLineAppService_Tests<TStartupModule> : InvoiceEvidenceApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IInvoiceAppService _invoiceAppService;
        private readonly IInvoiceLineAppService _invoiceLineAppService;

        protected InvoiceLineAppService_Tests()
        {
            _invoiceAppService = GetRequiredService<IInvoiceAppService>();
            _invoiceLineAppService = GetRequiredService<IInvoiceLineAppService>();
        }

        [Fact]
        public async Task Should_Add_Invoice_Line()
        {
            // Arrange
            var productName = "Tablet XP Pro 102";
            var quantity = 12;
            var unitPrice = 55.50m;

            // Act
            await _invoiceLineAppService.CreateInvoiceLineAsync(new CreateInvoiceLineDto()
            {
                InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                ProductName = productName,
                Quantity = quantity,
                UnitPrice = unitPrice
            });

            // Assert
            var invoice = await _invoiceAppService.GetInvoiceByIdAsync(InvoiceEvidenceTestConsts.CreatedInvoiceId);
            
            invoice.InvoiceLines.ShouldContain(
                x => x.ProductName == productName &&
                x.Quantity == quantity &&
                x.UnitPrice == unitPrice &&
                x.TotalPrice == quantity * unitPrice);
        }

        [Fact]
        public async Task Should_Throw_Not_Found_Exception_On_Add_Invoice_Line()
        {
            // Act + Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await _invoiceLineAppService.CreateInvoiceLineAsync(new CreateInvoiceLineDto()
                {
                    InvoiceId = Guid.Parse("bc1b20dd-0bff-44d4-b780-ca2b4227296c"), // Non-existing InvoiceId
                    ProductName = "Tablet XP Pro",
                    Quantity = 3,
                    UnitPrice = 10.50m
                });
            });
        }

        [Fact]
        public async Task Should_Update_Invoice_Line()
        {
            // Arrange
            var invoice = await _invoiceAppService.GetInvoiceByIdAsync(InvoiceEvidenceTestConsts.CreatedInvoiceId);
            var oldInvoiceLine = invoice.InvoiceLines.First(x => x.ProductId == InvoiceEvidenceTestConsts.CreatedInvoiceProductId1);

            var newProductName = "TV Samsung";
            var newQuantity = 6;
            var newUnitPrice = 12.65m;

            // Act
            await _invoiceLineAppService.UpdateInvoiceLineAsync(new UpdateInvoiceLineDto()
            {
                InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId1,
                ProductName = newProductName,
                UnitPrice = newUnitPrice,
                Quantity = newQuantity,
            });

            // Assert
            invoice = await _invoiceAppService.GetInvoiceByIdAsync(InvoiceEvidenceTestConsts.CreatedInvoiceId);
            var updatedInvoiceLine = invoice.InvoiceLines.First(x => x.ProductId == InvoiceEvidenceTestConsts.CreatedInvoiceProductId1);

            Assert.True(updatedInvoiceLine.ProductId == oldInvoiceLine.ProductId);
            Assert.False(updatedInvoiceLine.ProductName == oldInvoiceLine.ProductName);
            Assert.False(updatedInvoiceLine.Quantity == oldInvoiceLine.Quantity);
            Assert.False(updatedInvoiceLine.UnitPrice == oldInvoiceLine.UnitPrice);
            Assert.False(updatedInvoiceLine.TotalPrice == oldInvoiceLine.TotalPrice);

            Assert.True(updatedInvoiceLine.ProductName == newProductName);
            Assert.True(updatedInvoiceLine.Quantity == newQuantity);
            Assert.True(updatedInvoiceLine.UnitPrice == newUnitPrice);
            Assert.True(updatedInvoiceLine.TotalPrice == newQuantity * newUnitPrice);
        }

        [Fact]
        public async Task Should_Throw_Not_Found_Exception_On_Update_Invoice_Line()
        {
            // Act + Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await _invoiceLineAppService.UpdateInvoiceLineAsync(new UpdateInvoiceLineDto()
                {
                    InvoiceId = Guid.Parse("bc1b20dd-0bff-44d4-b780-ca2b4227296c"), // Non-existing InvoiceId
                    ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId1,
                    ProductName = "TV Samsung",
                    Quantity = 3,
                    UnitPrice = 10.50m
                });
            });
        }

        [Fact]
        public async Task Should_Throw_Not_Found_Exception_On_Update_Invoice_Line_2()
        {
            // Act + Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await _invoiceLineAppService.UpdateInvoiceLineAsync(new UpdateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                    ProductId = Guid.Parse("bc1b20dd-0bff-44d4-b780-ca2b4227296c"), // Non-existing ProductId
                    ProductName = "TV Samsung",
                    Quantity = 3,
                    UnitPrice = 10.50m
                });
            });
        }

        [Fact]
        public async Task Should_Delete_Invoice_Line()
        {
            // Arrange
            var invoice = await _invoiceAppService.GetInvoiceByIdAsync(InvoiceEvidenceTestConsts.CreatedInvoiceId);

            invoice.InvoiceLines.ShouldContain(
                x => x.ProductId == InvoiceEvidenceTestConsts.CreatedInvoiceProductId2); // Check that invoice line exists prior its deletion

            // Act
            await _invoiceLineAppService.DeleteInvoiceLineAsync(new DeleteInvoiceLineDto()
            {
                InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId2
            });

            // Assert
            invoice = await _invoiceAppService.GetInvoiceByIdAsync(InvoiceEvidenceTestConsts.CreatedInvoiceId);

            invoice.InvoiceLines.ShouldNotContain(
                x => x.ProductId == InvoiceEvidenceTestConsts.CreatedInvoiceProductId2);
        }

        [Fact]
        public async Task Should_Throw_Not_Found_Exception_On_Delete_Invoice_Line()
        {
            // Act + Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await _invoiceLineAppService.DeleteInvoiceLineAsync(new DeleteInvoiceLineDto()
                {
                    InvoiceId = Guid.Parse("bc1b20dd-0bff-44d4-b780-ca2b4227296c"), // Non-existing InvoiceId
                    ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId2
                });
            });
        }

        [Fact]
        public async Task Should_Not_Create_Invoice_Line_On_An_Invoice_In_Approved_State()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceLineAppService.CreateInvoiceLineAsync(new CreateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.ApprovedInvoiceId,
                    ProductName = "Tablet Samsung",
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task Should_Not_Create_Invoice_Line_On_An_Invoice_In_Paid_State()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceLineAppService.CreateInvoiceLineAsync(new CreateInvoiceLineDto()
                {
                    InvoiceId = InvoiceEvidenceTestConsts.PaidInvoiceId,
                    ProductName = "Tablet Samsung",
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task Should_Not_Update_Invoice_Line_On_An_Invoice_In_Approved_State()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceLineAppService.UpdateInvoiceLineAsync(new UpdateInvoiceLineDto()
                {
                    ProductId = InvoiceEvidenceTestConsts.ApprovedInvoiceProductId,
                    InvoiceId = InvoiceEvidenceTestConsts.ApprovedInvoiceId,
                    ProductName = "Tablet Samsung",
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task Should_Not_Update_Invoice_Line_On_An_Invoice_In_Paid_State()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async() =>
            {
                await _invoiceLineAppService.UpdateInvoiceLineAsync(new UpdateInvoiceLineDto()
                {
                    ProductId = InvoiceEvidenceTestConsts.PaidInvoiceProductId,
                    InvoiceId = InvoiceEvidenceTestConsts.PaidInvoiceId,
                    ProductName = "Tablet Samsung",
                    Quantity = 1,
                    UnitPrice = 10m
                });
            });
        }

        [Fact]
        public async Task Should_Not_Delete_Invoice_Line_On_An_Invoice_In_Approved_State()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceLineAppService.DeleteInvoiceLineAsync(new DeleteInvoiceLineDto()
                {
                    ProductId = InvoiceEvidenceTestConsts.ApprovedInvoiceProductId,
                    InvoiceId = InvoiceEvidenceTestConsts.ApprovedInvoiceId
                });
            });
        }

        [Fact]
        public async Task Should_Not_Delete_Invoice_Line_On_An_Invoice_In_Paid_State()
        {
            // Act + Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _invoiceLineAppService.DeleteInvoiceLineAsync(new DeleteInvoiceLineDto()
                {
                    ProductId = InvoiceEvidenceTestConsts.PaidInvoiceProductId,
                    InvoiceId = InvoiceEvidenceTestConsts.PaidInvoiceId
                });
            });
        }
    }
}
