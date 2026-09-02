namespace Backend.Domain;

// Ingen reference til Dapper, SQLite eller ASP.NET her.
public class Billede
{
    public int Id { get; set; }
    public string Navn { get; set; } = "";
    public string ContentType { get; set; } = "";
    public byte[] Data { get; set; } = [];
}
