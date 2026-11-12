using CentralizadorDescuentos.Modules.CatalogManagement.ViewModels;

namespace CentralizadorDescuentos.Modules.CatalogManagement.Services
{
    public interface IAiOfferExtractorService1
    {
        Task<ExtractedOfferDto> ExtractOfferDetailsAsync(string rawText);
    }
}