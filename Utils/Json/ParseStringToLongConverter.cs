using System.Text.Json;
using System.Text.Json.Serialization;

namespace BattleBitBrowser.Utils.Json;

public class ParseStringToLongConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return long.TryParse(reader.GetString(), out var value) ? value : 0;
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}