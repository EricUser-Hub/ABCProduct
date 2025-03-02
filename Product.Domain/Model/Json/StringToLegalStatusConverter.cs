using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Product.Domain.Enums;

public class StringToLegalStatusConverter : JsonConverter<LegalStatus>
{
    public override LegalStatus Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            // try to parse number directly from bytes
            //ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
            //if (Utf8Parser.TryParse(span, out long number, out int bytesConsumed) && span.Length == bytesConsumed)
            //    return number;

            // try to parse from a string if the above failed, this covers cases with other escaped/UTF characters
            return LegalStatusExtensions.GetValueFromDescription(reader.GetString());
        }

        return LegalStatus.None;
    }

    public override void Write(Utf8JsonWriter writer, LegalStatus value, JsonSerializerOptions options)
    {
        // TODO Eric
        writer.WriteStringValue(value.ToString());
    }
}