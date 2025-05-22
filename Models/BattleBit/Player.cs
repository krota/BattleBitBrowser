using System.Text.Json.Serialization;
using BattleBitBrowser.Utils.Json;

namespace BattleBitBrowser.Models.BattleBit;

public class Player
{
    public required string Name { get; set; }
    [JsonConverter(typeof(ParseStringToIntConverter))]
    public int Value { get; set; }
}