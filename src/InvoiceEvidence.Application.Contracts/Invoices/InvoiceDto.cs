using System;
using System.Collections.Generic;

namespace InvoiceEvidence.Invoices
{
    public class InvoiceDto : CreateInvoiceDto
    {
        public Guid InvoiceId { get; set; }

        public InvoiceState State { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<InvoiceLineDto> InvoiceLines { get; set; } = new List<InvoiceLineDto>();
    }
}
