using System;
using Volo.Abp.Domain.Entities;

namespace InvoiceEvidence.Invoices
{
    public class InvoiceLine : IEntity
    {
        public Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public Guid InvoiceId { get; set; }

        public object?[] GetKeys()
        {
            return [ProductId];
        }
    }
}
