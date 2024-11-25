using Exam891.Core.Serialization;

namespace Exam891.Core.Tests.Serialization
{
    public sealed class DerfaultSerializerTests
    {
        private sealed record class Person (string Name);

        [Fact]
        public void Serialize_WithDefaultOptions_CorrectString()
        {
            // Arrange
            var serializer = new DefaultSerializer();

            // Act
            var result = serializer.Serialize(new Person(Name: "Sotha Sil"));

            // Assert
            Assert.Equal("{\"name\":\"Sotha Sil\"}", result);
        }

        [Fact]
        public void Deserialize_WithDefaultOptions_CorrectObject()
        {
            // Arrange
            var serializer = new DefaultSerializer();

            // Act
            var result = serializer.Deserialize<Person>("{\"name\":\"Serjo Indoril Nerevar Mora\"}");

            // Assert
            Assert.Equal("Serjo Indoril Nerevar Mora", result?.Name);
        }
    }
}
