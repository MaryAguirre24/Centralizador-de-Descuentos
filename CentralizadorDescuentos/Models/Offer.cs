namespace CentralizadorDescuentos.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string RestrictionsJson { get; set; } = string.Empty; // Almacenado como JSON o string separado por |
        public string MinimumSpend { get; set; } = "Sin mínimo";
        public DateTime ExpirationDate { get; set; }
        public int ExpirationDaysLeft { get; set; }
        public string? PromoCode { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public DateTime LastVerified { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
