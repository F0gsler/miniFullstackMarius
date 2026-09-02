using Backend.Domain;

namespace Backend.Application;

// Kender kun interfacet - ikke Dapper, ikke SQLite.
public class BilledeService(IBilledeRepository repository)
{
    public Task<IEnumerable<Billede>> HentAlleAsync() => repository.HentAlleAsync();

    public Task<Billede?> HentAsync(int id) => repository.HentAsync(id);

    // Forretningsregel: er databasen tom, fyldes den med billederne fra mappen
    public async Task SeedAsync(string mappe)
    {
        if (await repository.AntalAsync() > 0 || !Directory.Exists(mappe)) return;

        foreach (var sti in Directory.GetFiles(mappe).OrderBy(p => p))
        {
            await repository.GemAsync(new Billede
            {
                Navn = Path.GetFileName(sti),
                ContentType = "image/jpeg",
                Data = await File.ReadAllBytesAsync(sti)
            });
        }
    }
}
