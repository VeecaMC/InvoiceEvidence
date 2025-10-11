using InvoiceEvidence.Samples;
using Xunit;

namespace InvoiceEvidence.EntityFrameworkCore.Domains;

[Collection(InvoiceEvidenceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<InvoiceEvidenceEntityFrameworkCoreTestModule>
{

}
