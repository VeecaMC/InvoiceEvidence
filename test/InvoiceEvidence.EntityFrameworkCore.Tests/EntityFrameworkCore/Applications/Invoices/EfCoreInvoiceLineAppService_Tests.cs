using InvoiceEvidence.Invoices;
using Xunit;

namespace InvoiceEvidence.EntityFrameworkCore.Applications.Invoices;

[Collection(InvoiceEvidenceTestConsts.CollectionDefinitionName)]
public class EfCoreInvoiceLineAppService_Tests : InvoiceLineAppService_Tests<InvoiceEvidenceEntityFrameworkCoreTestModule>
{

}