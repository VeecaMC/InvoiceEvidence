using Volo.Abp.Modularity;

namespace InvoiceEvidence;

[DependsOn(
    typeof(InvoiceEvidenceDomainModule),
    typeof(InvoiceEvidenceTestBaseModule)
)]
public class InvoiceEvidenceDomainTestModule : AbpModule
{

}
