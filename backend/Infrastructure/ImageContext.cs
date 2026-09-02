using Dapper;
using Microsoft.Data.Sqlite;

namespace Backend.Infrastructure;

// Holder forbindelsen til databasen. Kun infrastructure roerer den.
public class ImageContext
{
    public SqliteConnection Connection() => new("Data Source=galleri.db");

    public async Task OpretTabelAsync()
    {
        using var db = Connection();

        await db.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS Billeder (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                Navn        TEXT NOT NULL,
                ContentType TEXT NOT NULL,
                Data        BLOB NOT NULL
            );
            """);
    }
}
