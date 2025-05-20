namespace BattleBitBrowser.Models;

public class BattleBitServer
{
    public string? Name { get; set; }
    public string? Map { get; set; }
    public string? MapSize { get; set; }
    public string? Gamemode { get; set; }
    public string? Region { get; set; }
    public int Players { get; set; }
    public int QueuePlayers { get; set; }
    public int MaxPlayers { get; set; }
    public int Hz { get; set; }
    public string? DayNight { get; set; }
    public bool IsOfficial { get; set; }
    public bool HasPassword { get; set; }
    public string? AntiCheat { get; set; }
    public string? Build { get; set; }

    public bool IsFull => Players >= MaxPlayers;
    public string Tickrate => $"{Hz}Hz";
}