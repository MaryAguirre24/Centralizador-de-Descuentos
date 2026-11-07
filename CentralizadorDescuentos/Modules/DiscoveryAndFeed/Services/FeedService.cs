using CentralizadorDescuentos.Modules.DiscoveryAndFeed.ViewModels;
using CentralizadorDescuentos.Web.Shared.Data;

namespace CentralizadorDescuentos.Modules.DiscoveryAndFeed.Services
{
    public class FeedService
    {
        private readonly AppDbContext _db;

        public FeedService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<OfferCardDto>> GetPersonalizedFeedAsync()
        {
            return await _db.Offers
                .Where(o => o.IsActive && o.ExpirationDate >= DateTime.UtcNow)
                .Select(o => new OfferCardDto
                {
                    Id = o.Id,
                    StoreName = o.StoreName,
                    Title = o.Title,
                    Restrictions = o.KeyRestrictions.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList(),
                    MinimumSpend = o.MinimumSpend,
                    ExpirationDaysLeft = (o.ExpirationDate - DateTime.UtcNow).Days,
                    PromoCode = o.PromoCode,
                    OriginalUrl = o.OriginalUrl
                })
                .ToListAsync();
        }
    }
}
