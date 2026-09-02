using Backend.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ImageContext>();

var app = builder.Build();

var context = app.Services.GetRequiredService<ImageContext>();
await context.OpretTabelAsync();

// Laeg de 3 billeder fra wwwroot/images i databasen foerste gang
if (await context.AntalAsync() == 0)
{
    var mappe = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "images");

    foreach (var sti in Directory.GetFiles(mappe).OrderBy(p => p))
    {
        await context.GemAsync(new Billede
        {
            Navn = Path.GetFileName(sti),
            ContentType = "image/jpeg",
            Data = await File.ReadAllBytesAsync(sti)
        });
    }
}

// Listen af billeder
app.MapGet("/api/GetValue", async (ImageContext context) =>
{
    var billeder = await context.HentAlleAsync();
    return billeder.Select(b => new { navn = b.Navn, url = $"/api/billeder/{b.Id}" });
});

// Selve billedet, hentet fra databasen
app.MapGet("/api/billeder/{id:int}", async (int id, ImageContext context) =>
{
    var billede = await context.HentAsync(id);
    return billede is null ? Results.NotFound() : Results.File(billede.Data, billede.ContentType);
});

app.Run();