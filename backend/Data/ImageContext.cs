using Dapper;
using Microsoft.Data.Sqlite;

namespace Backend.Data;

public class Billede
{
    public int Id { get; set; }
    public string Navn { get; set; } = "";
    public string ContentType { get; set; } = "";
    public byte[] Data { get; set; } = [];
}

public class ImageContext
{
    private SqliteConnection Db() => new("Data Source=galleri.db");

    public async Task OpretTabelAsync()
    {
        using var db = Db();
        await db.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS Billeder (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                Navn        TEXT NOT NULL,
                ContentType TEXT NOT NULL,
                Data        BLOB NOT NULL
            );
            """);
    }

    public async Task<int> AntalAsync()
    {
        using var db = Db();
        return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Billeder;");
    }

    // Uden Data - vi henter ikke billedernes bytes til listen
    public async Task<IEnumerable<Billede>> HentAlleAsync()
    {
        using var db = Db();
        return await db.QueryAsync<Billede>("SELECT Id, Navn FROM Billeder ORDER BY Id;");
    }

    public async Task<Billede?> HentAsync(int id)
    {
        using var db = Db();
        return await db.QuerySingleOrDefaultAsync<Billede>(
            "SELECT Id, Navn, ContentType, Data FROM Billeder WHERE Id = @id;", new { id });
    }

    public async Task GemAsync(Billede billede)
    {
        using var db = Db();
        await db.ExecuteAsync(
            "INSERT INTO Billeder (Navn, ContentType, Data) VALUES (@Navn, @ContentType, @Data);",
            billede);
    }
}