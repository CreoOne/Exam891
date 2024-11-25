using System.Text.Json;
using System.Text.Json.Serialization;

namespace Exam891.Core.Serialization
{
    public sealed class DateOnlyConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyyMMdd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateOnly.ParseExact(reader.GetString()!, Format);

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(Format));
    }
}
