using InvoiceEvidence.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace InvoiceEvidence.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class InvoiceEvidenceController : AbpControllerBase
{
    protected InvoiceEvidenceController()
    {
        LocalizationResource = typeof(InvoiceEvidenceResource);
    }
}
