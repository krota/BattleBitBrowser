using BattleBitBrowser.Services;
using BattleBitBrowser.Services.Steam;
using BattleBitBrowser.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BattleBitBrowser.Controllers.Steam;

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