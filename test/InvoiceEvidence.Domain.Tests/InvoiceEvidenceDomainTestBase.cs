using Volo.Abp.Modularity;

namespace InvoiceEvidence;

/* Inherit from this class for your domain layer tests. */
public abstract class InvoiceEvidenceDomainTestBase<TStartupModule> : InvoiceEvidenceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
