using System.Text.Json;

namespace Exam891.Core.Serialization
{
    internal sealed class DefaultSerializer
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public DefaultSerializer(JsonSerializerOptions jsonSerializerOptions)
            => _jsonSerializerOptions = jsonSerializerOptions;

        public DefaultSerializer()
            : this(new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            })
        { }

        public string Serialize<T>(T value)
            => JsonSerializer.Serialize(value, _jsonSerializerOptions);

        public T? Deserialize<T>(string json)
            => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
    }
}
