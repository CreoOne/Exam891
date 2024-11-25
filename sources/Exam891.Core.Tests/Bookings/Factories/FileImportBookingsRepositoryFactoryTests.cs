using Exam891.Core.Bookings.Factories;

namespace Exam891.Core.Tests.Bookings.Factories
{
    public class FileImportBookingsRepositoryFactoryTests
    {
        [Fact]
        public void Create_WithValidFilePath_ReturnsBookingsRepository()
        {
            // Arrange
            var filePath = "bookings.json";

            // Act
            void Act() => FileImportBookingsRepositoryFactory.Create(filePath);

            // Assert
            Assert.Throws<FileNotFoundException>(Act);
        }
    }
}
