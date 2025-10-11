using System;

namespace InvoiceEvidence.Invoices
{
    public class InvoiceListItemDto
    {
        public Guid InvoiceId { get; set; }
        public long InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public InvoiceState State { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
