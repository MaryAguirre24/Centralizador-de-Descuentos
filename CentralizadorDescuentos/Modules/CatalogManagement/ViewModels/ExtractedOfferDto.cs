namespace CentralizadorDescuentos.Modules.CatalogManagement.ViewModels
{
    public class ExtractedOfferDto
    {
        public string StoreName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string> KeyRestrictions { get; set; } = new();
        public string MinimumSpend { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public string PromoCode { get; set; } = string.Empty;
        public string OriginalUrl { get; set; } = string.Empty;
    }
}
