var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

// wwwroot skal findes, ellers er WebRootPath null
var imageDir = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "images");
Directory.CreateDirectory(imageDir);

Console.WriteLine($"Billeder serveres fra: {imageDir}");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    // Kun i produktion - i dev roder den med Vite-proxyen
    app.UseHttpsRedirection();
}

app.UseStaticFiles();


// Returnerer listen af billeder til frontend
app.MapGet("/api/GetValue", () =>
{
    var billeder = Directory
        .EnumerateFiles(imageDir)
        .Where(p => IsImage(Path.GetExtension(p)))
        .OrderBy(p => p)
        .Select(p => new
        {
            navn = Path.GetFileName(p),
            url = $"/images/{Path.GetFileName(p)}"
        });

    return Results.Ok(billeder);
});


// Upload - gemmer nu i wwwroot/images sa filerne kan hentes bagefter
app.MapPost("/api/Postvalue", async (IFormFileCollection files) =>
{
    if (files.Count == 0)
        return Results.BadRequest(new { error = "Ingen filer modtaget." });

    var saved = new List<object>();

    foreach (var file in files)
    {
        if (file.Length == 0) continue;

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!IsImage(ext)) continue;

        var fileName = $"{Guid.NewGuid():N}{ext}";
        var path = Path.Combine(imageDir, fileName);

        await using var stream = File.Create(path);
        await file.CopyToAsync(stream);

        saved.Add(new { navn = fileName, url = $"/images/{fileName}" });
    }

    return Results.Ok(new { count = saved.Count, files = saved });
})
.Accepts<IFormFileCollection>("multipart/form-data")
.Produces<object>(StatusCodes.Status200OK)
.DisableAntiforgery();

app.Run();


static bool IsImage(string ext) =>
    ext is ".jpg" or ".jpeg" or ".png" or ".gif" or ".webp";