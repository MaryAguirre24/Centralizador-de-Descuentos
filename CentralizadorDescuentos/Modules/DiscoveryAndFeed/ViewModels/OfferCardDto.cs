namespace CentralizadorDescuentos.Modules.DiscoveryAndFeed.ViewModels
{
    public class OfferCardDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string> Restrictions { get; set; } = new();
        public string MinimumSpend { get; set; } = string.Empty;
        public int ExpirationDaysLeft { get; set; }
        public string PromoCode { get; set; } = string.Empty;
        public string OriginalUrl { get; set; } = string.Empty;
    }
}
