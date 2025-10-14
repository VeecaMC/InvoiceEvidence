using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Xunit;

namespace InvoiceEvidence.Invoices;

public abstract class InvoiceAppService_Tests<TStartupModule> : InvoiceEvidenceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IInvoiceAppService _invoiceAppService;

    protected InvoiceAppService_Tests()
    {
        _invoiceAppService = GetRequiredService<IInvoiceAppService>();
    }

    [Fact]
    public async Task Should_Get_List_Of_Invoices()
    {
        //Act
        var result = await _invoiceAppService.GetInvoicesListAsync(
            new GetInvoiceListDto()
            {
                SkipCount = 0,
                MaxResultCount = 10,
                Sorting = "IssueDate"
            }
        );

        //Assert
        result!.TotalCount.ShouldBe(3);
        result.Items.ShouldContain(b => b.InvoiceNumber == 1);
    }

    [Fact]
    public async Task Should_Get_Invoice_Detail()
    {
        // Act
        var result = await _invoiceAppService.GetInvoiceByIdAsync(InvoiceEvidenceTestConsts.CreatedInvoiceId);

        // Assert
        result.ShouldNotBeNull();
        result.InvoiceId.ShouldBe(InvoiceEvidenceTestConsts.CreatedInvoiceId);
        result.InvoiceNumber.ShouldBe(1L);
        result.IssueDate.ShouldBe(new DateTime(2000, 1, 1));
        result.State.ShouldBe(InvoiceState.Created);
        result.TotalAmount.ShouldBe(6m);
        result.InvoiceLines.ShouldNotBeNull();
        result.InvoiceLines.Count.ShouldBe(2);
    }

    [Fact]
    public async Task Should_Throw_Not_Found_Exception_On_Get_Invoice_Detail()
    {
        //Act + Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            var result = await _invoiceAppService
                .GetInvoiceByIdAsync(Guid.Parse("bc1b20dd-0bff-44d4-b780-ca2b4227296c"));  // Non-existing InvoiceId
        });
    }

    [Fact]
    public async Task Should_Change_Invoice_State()
    {
        //Act
        var result = await _invoiceAppService.UpdateInvoiceStateAsync(
            new UpdateInvoiceStateDto()
            {
                InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
                State = InvoiceState.Approved
            }
        );

        // Assert
        result.ShouldNotBeNull();
        result.State.ShouldNotBe(InvoiceState.Created);
        result.State.ShouldBe(InvoiceState.Approved);
    }

    [Fact]
    public async Task Should_Throw_Not_Found_Exception_On_Change_Invoice_State()
    {
        //Act + Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            var result = await _invoiceAppService.UpdateInvoiceStateAsync(
            new UpdateInvoiceStateDto()
            {
                InvoiceId = Guid.Parse("bc1b20dd-0bff-44d4-b780-ca2b4227296c"), // Non-existing InvoiceId
                State = InvoiceState.Approved
            });
        });
    }

    [Fact]
    public async Task Should_Create_A_Valid_Invoice()
    {
        // Arrange
        var issueDate = DateTime.UtcNow;

        //Act
        var result = await _invoiceAppService.CreateInvoiceAsync(
            new CreateInvoiceDto
            {
                IssueDate = issueDate,
                InvoiceNumber = 4
            }
        );

        //Assert
        result.ShouldNotBeNull();
        result.InvoiceId.ShouldNotBe(Guid.Empty);
        result.InvoiceNumber.ShouldBe(4);
        result.IssueDate.ShouldBe(issueDate);
        result.State.ShouldBe(InvoiceState.Created);
        result.InvoiceLines.ShouldNotBeNull();
        result.InvoiceLines.ShouldBeEmpty();
        result.TotalAmount.ShouldBe(0);
    }
}