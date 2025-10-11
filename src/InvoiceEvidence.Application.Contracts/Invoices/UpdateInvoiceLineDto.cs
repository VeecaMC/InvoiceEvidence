using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceEvidence.Invoices
{
    public class UpdateInvoiceLineDto : CreateInvoiceLineDto
    {
        [Required]
        public Guid ProductId { get; set; }

        public override IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            foreach (var validationResult in base.Validate(validationContext))
            {
                yield return validationResult;
            }

            if (ProductId == InvoiceId)
            {
                yield return new ValidationResult(
                    "ProductId cannot be the same as InvoiceId.",
                    new[] { "ProductId", "InvoiceId" }
                );
            }

            if (ProductId == Guid.Empty)
            {
                yield return new ValidationResult(
                    "ProductId cannot be an empty guid.",
                    new[] { "ProductId" }
                );
            }
        }
    }
}
