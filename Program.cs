using BattleBitBrowser.Settings;
using BattleBitBrowser.Services;
using BattleBitBrowser.Services.BattleBit;
using BattleBitBrowser.Services.Steam;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BattleBitSettings>(builder.Configuration.GetSection("BattleBit"));
builder.Services.AddHttpClient<IBattleBitService, BattleBitService>();
builder.Services.AddHttpClient("SteamApi", client =>
{
    client.BaseAddress = new Uri("http://steam-api:8081/");
});
builder.Services.AddScoped<ISteamApiService, SteamApiService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
