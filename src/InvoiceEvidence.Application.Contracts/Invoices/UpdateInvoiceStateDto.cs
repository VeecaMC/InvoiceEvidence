using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceEvidence.Invoices
{
    public class UpdateInvoiceStateDto : IValidatableObject
    {
        [Required]
        public Guid InvoiceId { get; set; }
        
        [Required]
        [AllowedValues([InvoiceState.Created, InvoiceState.Approved, InvoiceState.Paid])]
        public InvoiceState State {  get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (InvoiceId == Guid.Empty)
            {
                yield return new ValidationResult(
                    "InvoiceId cannot be an empty guid.",
                    new[] { "InvoiceId" }
                );
            }
        }
    }
}
