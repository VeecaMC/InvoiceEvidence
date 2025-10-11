using Volo.Abp.Modularity;

namespace InvoiceEvidence;

public abstract class InvoiceEvidenceApplicationTestBase<TStartupModule> : InvoiceEvidenceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
