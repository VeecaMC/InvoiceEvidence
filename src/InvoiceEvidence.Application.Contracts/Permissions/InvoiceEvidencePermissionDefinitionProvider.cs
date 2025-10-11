using InvoiceEvidence.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace InvoiceEvidence.Permissions;

public class InvoiceEvidencePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InvoiceEvidencePermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(InvoiceEvidencePermissions.MyPermission1, L("Permission:MyPermission1"));

        myGroup.AddPermission(InvoiceEvidencePermissions.Invoice.Default);
        myGroup.AddPermission(InvoiceEvidencePermissions.Invoice.Create);
        myGroup.AddPermission(InvoiceEvidencePermissions.Invoice.Edit);
        myGroup.AddPermission(InvoiceEvidencePermissions.Invoice.Delete);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InvoiceEvidenceResource>(name);
    }
}
