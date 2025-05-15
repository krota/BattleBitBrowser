using System.Text.Json;
using BattleBitBrowser.Models;
using BattleBitBrowser.Settings;
using Microsoft.Extensions.Options;

namespace BattleBitBrowser.Services;

public class BattleBitService(HttpClient httpClient, IOptions<BattleBitSettings> settings)
    : IBattleBitService
{
    public async Task<List<BattleBitServer>> GetServersAsync()
    {
        var response = await httpClient.GetAsync(settings.Value.ApiUrl);
        response.EnsureSuccessStatusCode();
        
        var rawJson = await response.Content.ReadAsStringAsync();
        var rawServers = JsonSerializer.Deserialize<List<BattleBitServer>>(rawJson);
        
        if (rawServers is null)
        {
            throw new InvalidOperationException("Failed to deserialize server list.");
        }
        
        return rawServers.Select(server =>
        {
            server.Gamemode = MapGameMode(server.Gamemode);
            return server;
        }).ToList();
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