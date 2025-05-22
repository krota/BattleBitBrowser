using System.Text.Json.Serialization;

namespace BattleBitBrowser.Models.BattleBit;

public class Server
{
    public required string Name { get; set; }
    public required string Map { get; set; }
    public required string MapSize { get; set; }
    public required string Gamemode { get; set; }
    public required string Region { get; set; }
    public int Players { get; set; }
    public int QueuePlayers { get; set; }
    public int MaxPlayers { get; set; }
    [JsonIgnore]
    public int Hz { get; set; }
    public required string DayNight { get; set; }
    public bool IsOfficial { get; set; }
    public bool HasPassword { get; set; }
    public required string AntiCheat { get; set; }
    public required string Build { get; set; }

    public bool IsFull => Players >= MaxPlayers;
    public string Tickrate => $"{Hz}Hz";
}