using BoardGamer.BoardGameGeek.BoardGameGeekXmlApi2;

namespace Frontend;

public class BggApiClient(HttpClient client)
{
    //get games
    public async Task<IEnumerable<CollectionResponse.Item>> GetGames()
    {
        return (await client.GetAsync("/games")
            .ContinueWith(a => a.Result.Content.ReadFromJsonAsync<IEnumerable<CollectionResponse.Item>>())
            .Unwrap());
    }

    public async Task<ThingResponse.Item> GetGame(int id)
    {
        return (await client.GetAsync($"/games/{id}")
            .ContinueWith(a => a.Result.Content.ReadFromJsonAsync<ThingResponse.Item>()).Unwrap());
    }
    //get game by Id
}