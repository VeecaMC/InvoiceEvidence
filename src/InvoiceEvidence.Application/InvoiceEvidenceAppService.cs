using InvoiceEvidence.Localization;
using Volo.Abp.Application.Services;

namespace InvoiceEvidence;

/* Inherit your application services from this class.
 */
public abstract class InvoiceEvidenceAppService : ApplicationService
{
    protected InvoiceEvidenceAppService()
    {
        LocalizationResource = typeof(InvoiceEvidenceResource);
    }
}
