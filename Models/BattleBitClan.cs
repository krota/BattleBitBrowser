using System.Text.Json.Serialization;
using BattleBitBrowser.Utils.Json;

namespace BattleBitBrowser.Models;

public class BattleBitClan
{
    public required string Clan { get; set; }
    
    public required string Tag { get; set; }
    
    [JsonConverter(typeof(ParseStringToLongConverter))]
    public long Xp { get; set; }
    
    [JsonConverter(typeof(ParseStringToIntConverter))]
    public int MaxPlayers { get; set; }
}