using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace InvoiceEvidence.Invoices
{
    public class Invoice : IEntity
    {
        public Guid InvoiceId { get; set; }
        public long InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public InvoiceState State { get; set; }

        public ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

        public object?[] GetKeys()
        {
            return [InvoiceId];
        }
    }
}
