using InvoiceEvidence.Invoices;
using Xunit;

namespace InvoiceEvidence.EntityFrameworkCore.Applications.Invoices;

[Collection(InvoiceEvidenceTestConsts.CollectionDefinitionName)]
public class EfCoreInvoiceAppService_Validation_Tests : InvoiceAppService_Validation_Tests<InvoiceEvidenceEntityFrameworkCoreTestModule>
{

}