using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public class CustomDateTimeConverter : JsonConverter<DateTime?>
{
    private const string Format = "dd-MM-yyyy";

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? dateString = reader.GetString();
        if (DateTime.TryParseExact(dateString, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            return date;
        }
        throw new JsonException($"Ngày không hợp lệ. Định dạng yêu cầu: {Format}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString(Format));
    }
}
