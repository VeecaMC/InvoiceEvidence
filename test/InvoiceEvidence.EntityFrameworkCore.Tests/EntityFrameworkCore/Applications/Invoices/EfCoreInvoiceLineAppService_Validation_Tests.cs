﻿using InvoiceEvidence.Invoices;
using Xunit;

namespace InvoiceEvidence.EntityFrameworkCore.Applications.Invoices;

[Collection(InvoiceEvidenceTestConsts.CollectionDefinitionName)]
public class EfCoreInvoiceLineAppService_Validation_Tests : InvoiceLineAppService_Validation_Tests<InvoiceEvidenceEntityFrameworkCoreTestModule>
{

}