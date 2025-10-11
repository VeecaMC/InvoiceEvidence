using AutoMapper;
using InvoiceEvidence.Invoices;

namespace InvoiceEvidence;

public class InvoiceEvidenceApplicationAutoMapperProfile : Profile
{
    public InvoiceEvidenceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Invoice, InvoiceListItemDto>();
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<CreateInvoiceDto, Invoice>();
        CreateMap<UpdateInvoiceStateDto, Invoice>();

        CreateMap<InvoiceLine, InvoiceLineDto>();
        CreateMap<CreateInvoiceLineDto, InvoiceLine>();
        CreateMap<UpdateInvoiceLineDto, InvoiceLine>();
    }
}
