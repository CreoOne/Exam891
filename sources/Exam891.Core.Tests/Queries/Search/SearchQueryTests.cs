using Exam891.Core.Bookings.Models;
using Exam891.Core.Bookings.Repositories;
using Exam891.Core.Hotels.Models;
using Exam891.Core.Hotels.Repositories;
using Exam891.Core.Queries.Search;

namespace Exam891.Core.Tests.Queries.Search
{
    public sealed class SearchQueryTests
    {
        [Fact]
        public void Search_WhenNoBookings_ReturnsNoResult()
        {
            // Arrange
            const string hotelId = "hotelId";
            const string roomType = "roomType";
            var hotelsRepository = new InMemoryHotelsRepository();
            hotelsRepository.Add(new Hotel
            {
                Id = hotelId,
                Name = "hotelName",
                Rooms =
                [
                    new Room
                    {
                        RoomType = roomType,
                        RoomId = "roomId"
                    }
                ]
            });

            var bookingsRepository = new NaiveBookingsRepository();
            var searchQuery = new SearchQuery(hotelsRepository, bookingsRepository);

            // Act
            var results = searchQuery.Search(hotelId, DateOnly.MinValue, DateOnly.MaxValue, roomType);

            // Assert
            Assert.Single(results);
        }

        [Fact]
        public void Search_WhenBookings_ReturnsResults()
        {
            // Arrange
            const string hotelId = "hotelId";
            const string roomType = "roomType";
            var hotelsRepository = new InMemoryHotelsRepository();
            hotelsRepository.Add(new Hotel
            {
                Id = hotelId,
                Name = "hotelName",
                Rooms =
                [
                    new Room
                    {
                        RoomType = roomType,
                        RoomId = "roomId"
                    }
                ]
            });
            var bookingsRepository = new NaiveBookingsRepository();
            bookingsRepository.Add(new Booking
            {
                HotelId = hotelId,
                RoomType = roomType,
                RoomRate = "roomRate",
                Arrival = new DateOnly(2027, 2, 5),
                Departure = new DateOnly(2027, 2, 7),
            });
            var searchQuery = new SearchQuery(hotelsRepository, bookingsRepository);

            // Act
            var results = searchQuery.Search(hotelId, new DateOnly(2027, 2, 1), new DateOnly(2027, 2, 20), roomType).ToArray();

            // Assert
            Assert.Equal(3, results.Length);
            Assert.Equal(new DateOnly(2027, 2, 1), results[0].From);
            Assert.Equal(new DateOnly(2027, 2, 5), results[0].To);
            Assert.Equal(1, results[0].AvailableRoomsCount);
            Assert.Equal(new DateOnly(2027, 2, 5), results[1].From);
            Assert.Equal(new DateOnly(2027, 2, 7), results[1].To);
            Assert.Equal(0, results[1].AvailableRoomsCount);
            Assert.Equal(new DateOnly(2027, 2, 7), results[2].From);
            Assert.Equal(new DateOnly(2027, 2, 20), results[2].To);
            Assert.Equal(1, results[2].AvailableRoomsCount);
        }
    }
}
