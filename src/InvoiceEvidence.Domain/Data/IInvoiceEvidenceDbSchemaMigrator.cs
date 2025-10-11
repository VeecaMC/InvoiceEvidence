using System.Threading.Tasks;

namespace InvoiceEvidence.Data;

public interface IInvoiceEvidenceDbSchemaMigrator
{
    Task MigrateAsync();
}
