using System.Text.Json;
using System.Text.Json.Serialization;

namespace BattleBitBrowser.Utils.Json;

public class ParseStringToIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return int.TryParse(reader.GetString(), out var value) ? value : 0;
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}