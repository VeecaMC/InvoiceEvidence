using InvoiceEvidence.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace InvoiceEvidence.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InvoiceEvidenceEntityFrameworkCoreModule),
    typeof(InvoiceEvidenceApplicationContractsModule)
)]
public class InvoiceEvidenceDbMigratorModule : AbpModule
{
}
