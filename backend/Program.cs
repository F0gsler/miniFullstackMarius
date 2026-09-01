var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/api/GetValue", () =>
{
    
});

app.MapPost("/api/Postvalue", async (IFormFileCollection files) =>
{
    if (files.Count == 0)
        return Results.BadRequest("No files uploaded.");

    var uploads = Path.Combine(app.Environment.ContentRootPath, "uploads");
    Directory.CreateDirectory(uploads);

    var saved = new List<string>();

    foreach (var file in files)
    {
        if (file.Length == 0) continue;

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var path = Path.Combine(uploads, fileName);

        await using var stream = File.Create(path);
        await file.CopyToAsync(stream);

        saved.Add(fileName);
    }

    return Results.Ok(new { count = saved.Count, files = saved });
})
.Accepts<IFormFileCollection>("multipart/form-data")
.Produces<object>(StatusCodes.Status200OK)
.DisableAntiforgery();

app.Run();

