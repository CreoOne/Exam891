using Exam891.Core.Hotels.Models;
using Exam891.Core.Hotels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam891.Core.Tests.Hotels.Repositories
{
    public sealed class NaiveHotelsRepositoryTests
    {
        [Fact]
        public void GetRoomCount_WhenNoRooms_ReturnsZero()
        {
            // Arrange
            var repository = new NaiveHotelsRepository();

            // Act
            var result = repository.GetRoomCount("hotelId", "roomType");

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetRoomCount_WhenRooms_ReturnsCount()
        {
            // Arrange
            var repository = new NaiveHotelsRepository();
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
