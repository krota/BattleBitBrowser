using System.Text.Json.Serialization;

namespace BattleBitBrowser.Models;
public class BattleBitLeaderboard
{
    public List<BattleBitClan> TopClans { get; set; } = [];
    public List<BattleBitPlayer> MostXp { get; set; } = [];
    public List<BattleBitPlayer> MostRevives { get; set; } = [];
    public List<BattleBitPlayer> MostVehiclesDestroyed { get; set; } = [];
    public List<BattleBitPlayer> MostRoadkills { get; set; } = [];
    public List<BattleBitPlayer> MostLongestKill { get; set; } = [];
    public List<BattleBitPlayer> MostObjectivesComplete { get; set; } = [];
    public List<BattleBitPlayer> MostKills { get; set; } = [];
}