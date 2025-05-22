namespace BattleBitBrowser.Models.BattleBit;
public class Leaderboard
{
    public List<PlayerClan> TopClans { get; set; } = [];
    public List<Player> MostXp { get; set; } = [];
    public List<Player> MostRevives { get; set; } = [];
    public List<Player> MostVehiclesDestroyed { get; set; } = [];
    public List<Player> MostRoadkills { get; set; } = [];
    public List<Player> MostLongestKill { get; set; } = [];
    public List<Player> MostObjectivesComplete { get; set; } = [];
    public List<Player> MostKills { get; set; } = [];
}