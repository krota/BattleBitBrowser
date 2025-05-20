using BattleBitBrowser.Models;

namespace BattleBitBrowser.Services;

public class SteamApiService(IHttpClientFactory factory) : ISteamApiService
{
    private readonly HttpClient _httpClient = factory.CreateClient("SteamApi");

    public async Task<int> GetCurrentPlayerCountAsync(int appId)
    {
        var res = await _httpClient.GetAsync($"/api/Steam/playercount/{appId}");
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<int>();
    }

    public async Task<List<SteamNewsItemViewModel>?> GetNewsAsync(int appId, int count = 3)
    {
        var res = await _httpClient.GetAsync($"/api/Steam/news/{appId}?count={count}");
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync <List<SteamNewsItemViewModel>>();
    }
}