using Shouldly;
using System;
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
            var productName = "Tablet XP Pro";
            var quantity = 3;
            var unitPrice = 10.50m;

            // Act
            var result = await _invoiceLineAppService.CreateInvoiceLineAsync(new CreateInvoiceLineDto()
            {
                InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                ProductName = productName,
                Quantity = quantity,
                UnitPrice = unitPrice
            });

            // Assert
            result.ShouldNotBeNull();
            result.ProductId.ShouldNotBe(Guid.Empty);
            result.ProductName.ShouldBe(productName);
            result.UnitPrice.ShouldBe(unitPrice);
            result.Quantity.ShouldBe(quantity);
            result.TotalPrice.ShouldBe(unitPrice * quantity);
        }

        [Fact]
        public async Task Should_Throw_Not_Found_Exception_On_Add_Invoice_Line()
        {
            // Act + Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                var result = await _invoiceLineAppService.CreateInvoiceLineAsync(new CreateInvoiceLineDto()
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
            var productName = "TV Samsung";
            var quantity = 3;
            var unitPrice = 10.50m;

            // Act
            var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(new UpdateInvoiceLineDto()
            {
                InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId1,
                ProductName = productName,
                UnitPrice = unitPrice,
                Quantity = quantity,
            });

            // Assert
            result.ShouldNotBeNull();
            result.ProductId.ShouldBe(InvoiceEvidenceTestConsts.CreatedInvoiceProductId1);
            result.ProductName.ShouldBe(productName);
            result.UnitPrice.ShouldBe(unitPrice);
            result.Quantity.ShouldBe(quantity);
            result.TotalPrice.ShouldBe(unitPrice * quantity);
        }

        [Fact]
        public async Task Should_Throw_Not_Found_Exception_On_Update_Invoice_Line()
        {
            // Act + Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(new UpdateInvoiceLineDto()
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
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(new UpdateInvoiceLineDto()
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
            // Act
            await _invoiceLineAppService.DeleteInvoiceLineAsync(new DeleteInvoiceLineDto()
            {
                InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId2
            });
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
                var result = await _invoiceLineAppService.CreateInvoiceLineAsync(new CreateInvoiceLineDto()
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
                var result = await _invoiceLineAppService.CreateInvoiceLineAsync(new CreateInvoiceLineDto()
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
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(new UpdateInvoiceLineDto()
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
                var result = await _invoiceLineAppService.UpdateInvoiceLineAsync(new UpdateInvoiceLineDto()
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
