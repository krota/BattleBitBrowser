using BattleBitBrowser.Models;
using BattleBitBrowser.Services;
using BattleBitBrowser.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BattleBitBrowser.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BattleBitController(IBattleBitService service) : ControllerBase
{
    [HttpGet("servers")]
    public async Task<IActionResult> GetServersAsync()
    {
        var servers = await service.GetServersAsync();
        return Ok(servers);
    }
    
    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetLeaderboardAsync()
    {
        var leaderboard = await service.GetLeaderboardAsync();
        return Ok(leaderboard);
    }
}

[ApiController]
[Route("api/[controller]")]
public class SteamController(ISteamApiService service, IOptions<BattleBitSettings> settings) : ControllerBase
{
    [HttpGet("playercount")]
    public async Task<IActionResult> GetPlayerCountAsync()
    {
        var steamAppId = settings.Value.SteamAppId;
        var count = await service.GetCurrentPlayerCountAsync(steamAppId);
        return Ok(count);
    }

    [HttpGet("news")]
    public async Task<IActionResult> GetNewsAsync([FromQuery] int count = 3)
    {
        var steamAppId = settings.Value.SteamAppId;
        var news = await service.GetNewsAsync(steamAppId, count);
        return Ok(news);
    }
}