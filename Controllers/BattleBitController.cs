using BattleBitBrowser.Models;
using BattleBitBrowser.Services;
using Microsoft.AspNetCore.Mvc;

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
}