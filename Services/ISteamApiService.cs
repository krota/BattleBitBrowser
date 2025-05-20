using BattleBitBrowser.Models;

namespace BattleBitBrowser.Services;

public interface ISteamApiService
{
    Task<int> GetCurrentPlayerCountAsync(int appId);
    Task<List<SteamNewsItemViewModel>?> GetNewsAsync(int appId, int count = 3);
}