using BattleBitBrowser.Models.BattleBit;

namespace BattleBitBrowser.Services.BattleBit;

public interface IBattleBitService
{
    Task<List<Server>> GetServersAsync();
    Task<Leaderboard> GetLeaderboardAsync();
}