using System.Text.Json;

namespace Exam891.Core.Serialization
{
    internal sealed class DefaultSerializer
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public DefaultSerializer()
        {
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            _jsonSerializerOptions.Converters.Add(new DateOnlyConverter());
        }

        public string Serialize<T>(T value)
            => JsonSerializer.Serialize(value, _jsonSerializerOptions);

        public T? Deserialize<T>(string json)
            => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
    }
}
