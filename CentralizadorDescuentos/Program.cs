using CentralizadorDescuentos.Models;
using CentralizadorDescuentos.Modules.CatalogManagement.Services;
using CentralizadorDescuentos.Modules.DiscoveryAndFeed.Services;
using CentralizadorDescuentos.Web.Shared.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Base de Datos SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=centralizador.db"));

// Inyección de Servicios de Módulos
builder.Services.AddScoped<IAiOfferExtractorService, AiOfferExtractorService>();
builder.Services.AddScoped<FeedService>();

// CORS para React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Asegurar creación de base de datos
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

// --- ENDPOINTS DISCOVERY & FEED ---
app.MapGet("/api/feed", async (FeedService feedService) =>
{
    var feed = await feedService.GetPersonalizedFeedAsync();
    return Results.Ok(feed);
});

// --- ENDPOINTS REDEMPTION & ANALYTICS ---
app.MapPost("/api/analytics/click", async (AppDbContext db, ClickEvent click) =>
{
    db.ClickEvents.Add(click);
    await db.SaveChangesAsync();
    return Results.Ok(new { status = "Event logged" });
});

// --- ENDPOINTS CATALOG MANAGEMENT (ADMIN & IA) ---
app.MapPost("/api/admin/extract-ai", async (IAiOfferExtractorService aiService, RawOfferInput input) =>
{
    var result = await aiService.ExtractOfferDetailsAsync(input.RawText);
    return Results.Ok(result);
});

app.MapPost("/api/admin/offers", async (AppDbContext db, Offer offer) =>
{
    db.Offers.Add(offer);
    await db.SaveChangesAsync();
    return Results.Created($"/api/offers/{offer.Id}", offer);
});

app.Run();

// DTO Auxiliar para entrada de IA
public record RawOfferInput(string RawText);