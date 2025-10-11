using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InvoiceEvidence.Data;
using Volo.Abp.DependencyInjection;

namespace InvoiceEvidence.EntityFrameworkCore;

public class EntityFrameworkCoreInvoiceEvidenceDbSchemaMigrator
    : IInvoiceEvidenceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreInvoiceEvidenceDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the InvoiceEvidenceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<InvoiceEvidenceDbContext>()
            .Database
            .MigrateAsync();
    }
}
