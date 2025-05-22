using BattleBitBrowser.Services;
using BattleBitBrowser.Services.BattleBit;
using Microsoft.AspNetCore.Mvc;

namespace BattleBitBrowser.Controllers.BattleBit;

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