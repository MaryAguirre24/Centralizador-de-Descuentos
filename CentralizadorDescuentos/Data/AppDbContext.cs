using CentralizadorDescuentos.Models;
using Microsoft.EntityFrameworkCore;

namespace CentralizadorDescuentos.Web.Shared.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<ClickEvent> ClickEvents => Set<ClickEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Datos semilla iniciales para pruebas locales
        modelBuilder.Entity<Offer>().HasData(
            new Offer
            {
                Id = 1,
                StoreName = "Burger King",
                Title = "2x1 en Combo Stacker Doble",
                RestrictionsJson = "Válido solo por App|No acumulable|Máximo 1 por cliente",
                MinimumSpend = "Sin mínimo",
                ExpirationDaysLeft = 2,
                PromoCode = "BK2X1STACKER",
                OriginalUrl = "https://www.burgerking.com.ar",
                LastVerified = DateTime.Now
            },
            new Offer
            {
                Id = 2,
                StoreName = "Mostaza",
                Title = "30% OFF en Mega Cuarto",
                RestrictionsJson = "Aplica en local y Take Away|Presentar cupón al pedir",
                MinimumSpend = "Mínimo: $3000",
                ExpirationDaysLeft = 5,
                PromoCode = "MOSTAZA30",
                OriginalUrl = "https://www.mostazaweb.com.ar",
                LastVerified = DateTime.Now
            }
        );
    }
}
