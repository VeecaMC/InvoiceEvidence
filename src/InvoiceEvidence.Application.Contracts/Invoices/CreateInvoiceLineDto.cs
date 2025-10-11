using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceEvidence.Invoices
{
    public class CreateInvoiceLineDto : IValidatableObject
    {
        [Required]
        public Guid InvoiceId { get; set; }

        [Required]
        [StringLength(128)]
        public required string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (InvoiceId == Guid.Empty)
            {
                yield return new ValidationResult(
                    "InvoiceId cannot be an empty guid.",
                    new[] { "InvoiceId" }
                );
            }

            if (ProductName == string.Empty)
            {
                yield return new ValidationResult(
                    "ProductName cannot be an empty string.",
                    new[] { "ProductName" }
                );
            }

            if(Quantity <= 0)
            {
                yield return new ValidationResult(
                    "Quantity cannot be less than or equal to zero.",
                    new[] {"Quantity"}
                );
            }
        }
    }
}
