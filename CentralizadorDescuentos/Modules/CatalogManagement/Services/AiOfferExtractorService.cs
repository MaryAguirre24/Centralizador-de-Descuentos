using CentralizadorDescuentos.Modules.CatalogManagement.ViewModels;

namespace CentralizadorDescuentos.Modules.CatalogManagement.Services
{
    public interface IAiOfferExtractorService
    {
        Task<ExtractedOfferDto> ExtractOfferDetailsAsync(string rawText);
    }
    public class AiOfferExtractorService : IAiOfferExtractorService
    {
        public async Task<ExtractedOfferDto> ExtractOfferDetailsAsync(string rawText)
        {
            // Simulación de procesamiento por IA / Prompting para estructurar el texto crudo
            await Task.Delay(300); // Latencia simulada

            var extracted = new ExtractedOfferDto
            {
                StoreName = rawText.Contains("Burger", StringComparison.OrdinalIgnoreCase) ? "Burger King" : "Comercio",
                Title = "Promoción Destacada Extraída por IA",
                KeyRestrictions = new List<string>
            {
                "Válido por tiempo limitado",
                "Sujeto a disponibilidad de stock",
                "Máximo 1 uso por usuario"
            },
                MinimumSpend = "Sin mínimo",
                ExpirationDate = DateTime.UtcNow.AddDays(3),
                PromoCode = "IAPROMO2026",
                OriginalUrl = "https://oferta-original.com"
            };

            return extracted;
        }
    }
}
