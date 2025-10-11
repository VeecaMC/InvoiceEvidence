using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace InvoiceEvidence;

public class InvoiceEvidenceDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{

    public InvoiceEvidenceDataSeederContributor()
    {

    }

    public Task SeedAsync(DataSeedContext context)
    {
        return Task.CompletedTask;
    }
}