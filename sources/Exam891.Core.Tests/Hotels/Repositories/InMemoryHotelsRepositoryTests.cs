using Exam891.Core.Hotels.Models;
using Exam891.Core.Hotels.Repositories;

namespace Exam891.Core.Tests.Hotels.Repositories
{
    public sealed class InMemoryHotelsRepositoryTests
    {
        [Fact]
        public void GetRoomCount_WhenNoRooms_ReturnsZero()
        {
            // Arrange
            var repository = new InMemoryHotelsRepository();

            // Act
            var result = repository.GetRoomCount("hotelId", "roomType");

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetRoomCount_WhenRooms_ReturnsCount()
        {
            // Arrange
            var repository = new InMemoryHotelsRepository();
            repository.Add(new Hotel
            {
                Id = "hotelId",
                Name = "hotelName",
                Rooms =
                [
                    new Room
                    {
                        RoomType = "roomType",
                        RoomId = "roomId"
                    }
                ]
            });

            // Act
            var result = repository.GetRoomCount("hotelId", "roomType");

            // Assert
            Assert.Equal(1, result);
        }
    }
}
