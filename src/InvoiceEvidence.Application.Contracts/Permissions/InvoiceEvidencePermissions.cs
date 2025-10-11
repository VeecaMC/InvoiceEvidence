namespace InvoiceEvidence.Permissions;

public static class InvoiceEvidencePermissions
{
    public const string GroupName = "InvoiceEvidence";

    public static class Invoice
    {
        public const string Default = GroupName + ".Invoices";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
