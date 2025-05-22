using System.Text.Json;
using BattleBitBrowser.Models.BattleBit;
using BattleBitBrowser.Settings;
using Microsoft.Extensions.Options;

namespace BattleBitBrowser.Services.BattleBit;

public class BattleBitService(HttpClient httpClient, IOptions<BattleBitSettings> settings)
    : IBattleBitService
{
    public async Task<List<Server>> GetServersAsync()
    {
        var apiUrl = $"{settings.Value.ApiUrlBase}{settings.Value.ServerApiUri}";
        var response = await httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        
        var rawJson = await response.Content.ReadAsStringAsync();
        var rawServers = JsonSerializer.Deserialize<List<Server>>(rawJson);
        
        if (rawServers is null)
        {
            throw new InvalidOperationException("Failed to deserialize server list.");
        }
        
        return rawServers.Select(server =>
        {
            server.Gamemode = MapGameMode(server.Gamemode ?? string.Empty);
            return server;
        }).ToList();
    }

    public async Task<Leaderboard> GetLeaderboardAsync()
    {
        var apiUrl = $"{settings.Value.ApiUrlBase}{settings.Value.LeaderboardApiUri}";
        var response = await httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        using var doc = await JsonDocument.ParseAsync(stream);

        var leaderboard = new Leaderboard();

        foreach (var element in doc.RootElement.EnumerateArray())
        {
            foreach (var property in element.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "TopClans":
                        leaderboard.TopClans = JsonSerializer.Deserialize<List<PlayerClan>>(property.Value.GetRawText())!;
                        break;
                    case "MostXP":
                        leaderboard.MostXp = JsonSerializer.Deserialize<List<Player>>(property.Value.GetRawText())!;
                        break;
                    case "MostRevives":
                        leaderboard.MostRevives = JsonSerializer.Deserialize<List<Player>>(property.Value.GetRawText())!;
                        break;
                    case "MostVehiclesDestroyed":
                        leaderboard.MostVehiclesDestroyed = JsonSerializer.Deserialize<List<Player>>(property.Value.GetRawText())!;
                        break;
                    case "MostRoadkills":
                        leaderboard.MostRoadkills = JsonSerializer.Deserialize<List<Player>>(property.Value.GetRawText())!;
                        break;
                    case "MostLongestKill":
                        leaderboard.MostLongestKill = JsonSerializer.Deserialize<List<Player>>(property.Value.GetRawText())!;
                        break;
                    case "MostObjectivesComplete":
                        leaderboard.MostObjectivesComplete = JsonSerializer.Deserialize<List<Player>>(property.Value.GetRawText())!;
                        break;
                    case "MostKills":
                        leaderboard.MostKills = JsonSerializer.Deserialize<List<Player>>(property.Value.GetRawText())!;
                        break;
                }
            }
        }

        return leaderboard;
    }

    private string MapGameMode(string gameMode) =>
        gameMode switch
        {
            "CONQ" => "Conquest",
            "INFCONQ" => "Infantry Conquest",
            "RUSH" => "Rush",
            "DOMI" => "Domination",
            "FRONTLINE" => "Frontline",
            "VoxelFortify" => "Voxel Fortify",
            _ => gameMode
        };
}