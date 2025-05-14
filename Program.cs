using BattleBitBrowser.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BattleBitSettings>(builder.Configuration.GetSection("BattleBitSettings"));

builder.Services.AddHttpClient();

var app = builder.Build();

Console.WriteLine("API URL: " + builder.Configuration["BattleBit:ApiUrl"]);

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/servers", async (
        IHttpClientFactory httpClientFactory,
        IOptions<BattleBitSettings> settings)
    =>
    {
        var apiUrl = settings.Value.ApiUrl;

        Console.WriteLine(apiUrl);

        if (string.IsNullOrEmpty(apiUrl))
        {
            return Results.Problem("BattleBit API URL is not configured.");
        }

        var client = httpClientFactory.CreateClient();
        var response = await client.GetAsync(apiUrl);

        if (!response.IsSuccessStatusCode)
        {
            return Results.Problem("Failed to fetch BattleBit Server List");
        }
        
        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json");

    });

app.Run();
