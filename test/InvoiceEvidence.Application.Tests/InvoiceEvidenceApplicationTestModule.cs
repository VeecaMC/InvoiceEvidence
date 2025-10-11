using Volo.Abp.Modularity;

namespace InvoiceEvidence;

[DependsOn(
    typeof(InvoiceEvidenceApplicationModule),
    typeof(InvoiceEvidenceDomainTestModule)
)]
public class InvoiceEvidenceApplicationTestModule : AbpModule
{

}
