using BattleBitBrowser.Models;

namespace BattleBitBrowser.Services;

public interface IBattleBitService
{
    Task<List<BattleBitServer>> GetServersAsync();
    Task<BattleBitLeaderboard> GetLeaderboardAsync();
}