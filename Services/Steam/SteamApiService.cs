using BattleBitBrowser.Models.Steam;

namespace BattleBitBrowser.Services.Steam;

public class SteamApiService(IHttpClientFactory factory) : ISteamApiService
{
    private readonly HttpClient _httpClient = factory.CreateClient("SteamApi");

    public async Task<PlayerCountViewModel?> GetCurrentPlayerCountAsync(int appId)
    {
        var res = await _httpClient.GetAsync($"/api/Steam/players/{appId}");
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<PlayerCountViewModel>();
    }

    public async Task<List<NewsItemViewModel>?> GetNewsAsync(int appId, int count = 3)
    {
        var res = await _httpClient.GetAsync($"/api/Steam/news/{appId}?count={count}");
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync <List<NewsItemViewModel>>();
    }
}