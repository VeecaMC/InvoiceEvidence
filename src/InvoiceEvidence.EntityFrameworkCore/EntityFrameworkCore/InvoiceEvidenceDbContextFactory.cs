using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InvoiceEvidence.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class InvoiceEvidenceDbContextFactory : IDesignTimeDbContextFactory<InvoiceEvidenceDbContext>
{
    public InvoiceEvidenceDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        InvoiceEvidenceEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<InvoiceEvidenceDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new InvoiceEvidenceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../InvoiceEvidence.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
