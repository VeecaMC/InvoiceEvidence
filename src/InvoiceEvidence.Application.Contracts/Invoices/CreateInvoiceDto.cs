using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceEvidence.Invoices
{
    public class CreateInvoiceDto : IValidatableObject
    {
        [Required]
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        [Required]
        public long InvoiceNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(InvoiceNumber <= 0)
            {
                yield return new ValidationResult(
                    "InvoiceNumber cannot be less than or equal to zero.",
                    new[] { "InvoiceNumber" }
                );
            }
        }
    }
}
