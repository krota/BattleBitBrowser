using BattleBitBrowser.Models.Steam;

namespace BattleBitBrowser.Services.Steam;

public interface ISteamApiService
{
    Task<PlayerCountViewModel?> GetCurrentPlayerCountAsync(int appId);
    Task<List<NewsItemViewModel>?> GetNewsAsync(int appId, int count = 3);
}