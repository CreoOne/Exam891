using Exam891.Core.Serialization;

namespace Exam891.Core.Tests.Serialization
{
    public sealed class DerfaultSerializerTests
    {
        private sealed record class Person (string Name, DateOnly DateOfBirth);

        [Fact]
        public void Serialize_WithDefaultOptions_CorrectString()
        {
            // Arrange
            var serializer = new DefaultSerializer();

            // Act
            var result = serializer.Serialize(new Person(Name: "Sotha Sil", DateOfBirth: new DateOnly(301, 4, 15)));

            // Assert
            Assert.Equal("{\"name\":\"Sotha Sil\",\"dateOfBirth\":\"03010415\"}", result);
        }

        [Fact]
        public void Deserialize_WithDefaultOptions_CorrectObject()
        {
            // Arrange
            var serializer = new DefaultSerializer();

            // Act
            var result = serializer.Deserialize<Person>("{\"name\":\"Serjo Indoril Nerevar Mora\",\"dateOfBirth\":\"03010604\"}");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Serjo Indoril Nerevar Mora", result.Name);
            Assert.Equal(new DateOnly(301, 6, 4), result.DateOfBirth);
        }
    }
}
