using InvoiceEvidence.Invoices;
using Xunit;

namespace InvoiceEvidence.EntityFrameworkCore.Applications.Invoices;

[Collection(InvoiceEvidenceTestConsts.CollectionDefinitionName)]
public class EfCoreInvoiceAppService_Tests : InvoiceAppService_Tests<InvoiceEvidenceEntityFrameworkCoreTestModule>
{

}