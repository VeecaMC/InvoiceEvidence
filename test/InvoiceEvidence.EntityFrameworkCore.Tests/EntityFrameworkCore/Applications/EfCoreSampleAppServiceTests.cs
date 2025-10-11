using InvoiceEvidence.Samples;
using Xunit;

namespace InvoiceEvidence.EntityFrameworkCore.Applications;

[Collection(InvoiceEvidenceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<InvoiceEvidenceEntityFrameworkCoreTestModule>
{

}
