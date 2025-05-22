using System.Text.Json;
using System.Text.Json.Serialization;
using BattleBitBrowser.Utils.Json;

namespace BattleBitBrowser.Models;

public class BattleBitPlayer
{
    public required string Name { get; set; }
    [JsonConverter(typeof(ParseStringToIntConverter))]
    public int Value { get; set; }
}