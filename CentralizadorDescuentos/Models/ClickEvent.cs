namespace CentralizadorDescuentos.Models
{
    public class ClickEvent
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public string ActionType { get; set; } = string.Empty; // "COPY_CODE" o "REDIRECT"
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
