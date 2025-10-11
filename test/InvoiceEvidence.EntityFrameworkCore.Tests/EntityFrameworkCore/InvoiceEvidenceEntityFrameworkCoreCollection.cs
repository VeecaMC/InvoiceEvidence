using Xunit;

namespace InvoiceEvidence.EntityFrameworkCore;

[CollectionDefinition(InvoiceEvidenceTestConsts.CollectionDefinitionName)]
public class InvoiceEvidenceEntityFrameworkCoreCollection : ICollectionFixture<InvoiceEvidenceEntityFrameworkCoreFixture>
{

}
