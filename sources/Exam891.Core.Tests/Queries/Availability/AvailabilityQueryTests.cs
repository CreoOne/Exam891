using Exam891.Core.Bookings.Models;
using Exam891.Core.Bookings.Repositories;
using Exam891.Core.Hotels.Models;
using Exam891.Core.Hotels.Repositories;
using Exam891.Core.Queries.Availability;
using Exam891.Core.Queries.Search;

namespace Exam891.Core.Tests.Queries.Availability
{
    public sealed class AvailabilityQueryTests
    {
        [Fact]
        public void GetAvailability_WhenNoBookings_ReturnsAllRooms()
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
            var availabilityQuery = new AvailabilityQuery(searchQuery);

            // Act
            var results = availabilityQuery.Availability(hotelId, DateOnly.MinValue, DateOnly.MaxValue, roomType);

            // Assert
            Assert.Equal(1, results);
        }

        [Fact]
        public void GetAvailability_WhenBookings_ReturnsAvailableRooms()
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
                Arrival = DateOnly.MinValue,
                Departure = DateOnly.MaxValue,
                RoomType = roomType,
                RoomRate = "roomRate"
            });
            var searchQuery = new SearchQuery(hotelsRepository, bookingsRepository);
            var availabilityQuery = new AvailabilityQuery(searchQuery);
            // Act
            var results = availabilityQuery.Availability(hotelId, DateOnly.MinValue, DateOnly.MaxValue, roomType);
            // Assert
            Assert.Equal(0, results);
        }
    }
}
