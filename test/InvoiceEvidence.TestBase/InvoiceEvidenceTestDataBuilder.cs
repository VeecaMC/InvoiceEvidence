using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using InvoiceEvidence.Invoices;
using Volo.Abp.Domain.Repositories;

namespace InvoiceEvidence;

public class InvoiceEvidenceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;

    private readonly IRepository<Invoice> _invoiceRepository;
    private readonly IRepository<InvoiceLine> _invoiceLineRepository;

    public InvoiceEvidenceTestDataSeedContributor(
        ICurrentTenant currentTenant, 
        IRepository<Invoice> invoiceRepository, 
        IRepository<InvoiceLine> invoiceLineRepository)
    {
        _currentTenant = currentTenant;
        _invoiceRepository = invoiceRepository;
        _invoiceLineRepository = invoiceLineRepository;
    }

    public Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */

        // Invoices
        _invoiceRepository.InsertAsync(new Invoice()
        {
            InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
            IssueDate = new DateTime(2000,1,1),
            InvoiceNumber = 1L,
            State = InvoiceState.Created,
            TotalAmount = 6m,
        });

        _invoiceRepository.InsertAsync(new Invoice()
        {
            InvoiceId = InvoiceEvidenceTestConsts.ApprovedInvoiceId,
            IssueDate = new DateTime(2000, 1, 1),
            InvoiceNumber = 2L,
            State = InvoiceState.Approved,
            TotalAmount = 21m,
        });

        _invoiceRepository.InsertAsync(new Invoice()
        {
            InvoiceId = InvoiceEvidenceTestConsts.PaidInvoiceId,
            IssueDate = new DateTime(2000, 1, 1),
            InvoiceNumber = 3L,
            State = InvoiceState.Paid,
            TotalAmount = 21m,
        });

        // Invoice lines
        _invoiceLineRepository.InsertAsync(new InvoiceLine()
        {
            ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId1,
            InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
            ProductName = "Tablet XP Pro 1",
            Quantity = 2,
            UnitPrice = 1m,
            TotalPrice = 2m

        });

        _invoiceLineRepository.InsertAsync(new InvoiceLine()
        {
            ProductId = InvoiceEvidenceTestConsts.CreatedInvoiceProductId2,
            InvoiceId = InvoiceEvidenceTestConsts.CreatedInvoiceId,
            ProductName = "Tablet XP Pro 2",
            Quantity = 2,
            UnitPrice = 2m,
            TotalPrice = 4m

        });

        _invoiceLineRepository.InsertAsync(new InvoiceLine()
        {
            ProductId = InvoiceEvidenceTestConsts.ApprovedInvoiceProductId,
            InvoiceId = InvoiceEvidenceTestConsts.ApprovedInvoiceId,
            ProductName = "Tablet XP Pro",
            Quantity = 2,
            UnitPrice = 10.50m,
            TotalPrice = 21m
            
        });

        _invoiceLineRepository.InsertAsync(new InvoiceLine()
        {
            ProductId = InvoiceEvidenceTestConsts.PaidInvoiceProductId,
            InvoiceId = InvoiceEvidenceTestConsts.PaidInvoiceId,
            ProductName = "Tablet XP Pro",
            Quantity = 2,
            UnitPrice = 10.50m,
            TotalPrice = 21m

        });

        using (_currentTenant.Change(context?.TenantId))
        {
            return Task.CompletedTask;
        }
    }
}
