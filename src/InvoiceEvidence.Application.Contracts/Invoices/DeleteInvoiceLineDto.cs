using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceEvidence.Invoices
{
    public class DeleteInvoiceLineDto : IValidatableObject
    {
        [Required]
        public Guid InvoiceId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (InvoiceId == ProductId)
            {
                yield return new ValidationResult(
                    "InvoiceId cannot be the same as ProductId.",
                    new[] { "InvoiceId", "ProductId" }
                );
            }

            if (InvoiceId == Guid.Empty)
            {
                yield return new ValidationResult(
                    "InvoiceId cannot be an empty guid.",
                    new[] { "InvoiceId" }
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
