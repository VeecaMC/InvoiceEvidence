using System;

namespace InvoiceEvidence.Invoices
{
    public  class InvoiceLineDto
    {
        public Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
