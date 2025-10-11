using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace InvoiceEvidence.Data;

/* This is used if database provider does't define
 * IInvoiceEvidenceDbSchemaMigrator implementation.
 */
public class NullInvoiceEvidenceDbSchemaMigrator : IInvoiceEvidenceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
