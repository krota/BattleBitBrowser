using System.Text.Json.Serialization;
using BattleBitBrowser.Utils.Json;

namespace BattleBitBrowser.Models.BattleBit;

public class PlayerClan
{
    public required string Clan { get; set; }
    
    public required string Tag { get; set; }
    
    [JsonConverter(typeof(ParseStringToLongConverter))]
    public long XP { get; set; }
    
    [JsonConverter(typeof(ParseStringToIntConverter))]
    public int MaxPlayers { get; set; }
}