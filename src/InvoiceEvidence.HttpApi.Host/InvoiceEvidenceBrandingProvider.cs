using Microsoft.Extensions.Localization;
using InvoiceEvidence.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace InvoiceEvidence;

[Dependency(ReplaceServices = true)]
public class InvoiceEvidenceBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<InvoiceEvidenceResource> _localizer;

    public InvoiceEvidenceBrandingProvider(IStringLocalizer<InvoiceEvidenceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
