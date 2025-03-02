using System.Text.Json;
using System.Text.Json.Serialization;
using Product.Domain.Enums;

public class StringToLegalStatusConverter : JsonConverter<LegalStatus>
{
    public override LegalStatus Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
            return LegalStatusExtensions.GetValueFromDescription(reader.GetString());
        
        return LegalStatus.None;
    }

    public override void Write(Utf8JsonWriter writer, LegalStatus value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}