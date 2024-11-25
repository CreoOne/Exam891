using Exam891.Core.Hotels.Factories;

namespace Exam891.Core.Tests.Hotels.Factories
{
    public class FileImportHotelsRepositoryFactoryTests
    {
        [Fact]
        public void Create_WithValidFilePath_ReturnsHotelsRepository()
        {
            // Arrange
            var filePath = "hotels.json";

            // Act
            void Act() => FileImportHotelsRepositoryFactory.Create(filePath);

            // Assert
            Assert.Throws<FileNotFoundException>(Act);
        }
    }
}
