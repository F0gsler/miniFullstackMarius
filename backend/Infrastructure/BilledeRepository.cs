using Backend.Domain;
using Dapper;

namespace Backend.Infrastructure;

// Implementerer domaenets interface med Dapper.
public class BilledeRepository(ImageContext context) : IBilledeRepository
{
    public async Task<int> AntalAsync()
    {
        using var db = context.Connection();
        return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Billeder;");
    }

    // Uden Data - vi henter ikke billedernes bytes til listen
    public async Task<IEnumerable<Billede>> HentAlleAsync()
    {
        using var db = context.Connection();
        return await db.QueryAsync<Billede>("SELECT Id, Navn FROM Billeder ORDER BY Id;");
    }

    public async Task<Billede?> HentAsync(int id)
    {
        using var db = context.Connection();

        return await db.QuerySingleOrDefaultAsync<Billede>(
            "SELECT Id, Navn, ContentType, Data FROM Billeder WHERE Id = @id;", new { id });
    }

    public async Task GemAsync(Billede billede)
    {
        using var db = context.Connection();

        await db.ExecuteAsync(
            "INSERT INTO Billeder (Navn, ContentType, Data) VALUES (@Navn, @ContentType, @Data);",
            billede);
    }
}
