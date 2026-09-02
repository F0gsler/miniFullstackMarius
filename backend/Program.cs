using Backend.Application;
using Backend.Domain;
using Backend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Composition root - her bindes interface til implementation
builder.Services.AddSingleton<ImageContext>();
builder.Services.AddSingleton<IBilledeRepository, BilledeRepository>();
builder.Services.AddSingleton<BilledeService>();

var app = builder.Build();

await app.Services.GetRequiredService<ImageContext>().OpretTabelAsync();
await app.Services.GetRequiredService<BilledeService>()
    .SeedAsync(Path.Combine(app.Environment.ContentRootPath, "wwwroot", "images"));

// Listen af billeder
app.MapGet("/api/GetValue", async (BilledeService service) =>
{
    var billeder = await service.HentAlleAsync();
    return billeder.Select(b => new { navn = b.Navn, url = $"/api/billeder/{b.Id}" });
});

// Selve billedet, hentet fra databasen
app.MapGet("/api/billeder/{id:int}", async (int id, BilledeService service) =>
{
    var billede = await service.HentAsync(id);
    return billede is null ? Results.NotFound() : Results.File(billede.Data, billede.ContentType);
});

app.Run();
