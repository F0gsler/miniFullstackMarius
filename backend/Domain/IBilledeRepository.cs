namespace Backend.Domain;

// Domaenet bestemmer HVAD der kan lade sig goere.
// Infrastructure bestemmer HVORDAN det sker.
public interface IBilledeRepository
{
    Task<int> AntalAsync();
    Task<IEnumerable<Billede>> HentAlleAsync();
    Task<Billede?> HentAsync(int id);
    Task GemAsync(Billede billede);
}
