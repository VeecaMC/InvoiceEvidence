using Volo.Abp.Settings;

namespace InvoiceEvidence.Settings;

public class InvoiceEvidenceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(InvoiceEvidenceSettings.MySetting1));
    }
}
