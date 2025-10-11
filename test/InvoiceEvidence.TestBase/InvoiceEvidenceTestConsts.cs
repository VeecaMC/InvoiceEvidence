using System;

namespace InvoiceEvidence;

public static class InvoiceEvidenceTestConsts
{
    public const string CollectionDefinitionName = "InvoiceEvidence collection";

    public static Guid CreatedInvoiceId = Guid.Parse("d8947139-4a25-4d10-be14-912b331a3df0");
    public static Guid CreatedInvoiceProductId1 = Guid.Parse("77dbed7e-5435-4c7e-ab9c-37d49f60eea6");
    public static Guid CreatedInvoiceProductId2 = Guid.Parse("cfa39652-77fa-4815-985c-c4c6be64b705");

    public static Guid ApprovedInvoiceId = Guid.Parse("ff9d01e9-fc1b-4dda-9be3-bccdee49a493");
    public static Guid ApprovedInvoiceProductId = Guid.Parse("1f6e0cdb-d6b9-49be-9eb4-d78bb2f4259b");

    public static Guid PaidInvoiceId = Guid.Parse("6205d6e9-d8a9-4c6d-8912-f0b47c2d7ff4");
    public static Guid PaidInvoiceProductId = Guid.Parse("1739584d-53d1-4195-9775-22672bd4b4fc");
}
