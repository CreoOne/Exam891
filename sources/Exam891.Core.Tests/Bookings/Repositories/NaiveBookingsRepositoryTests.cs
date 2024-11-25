using Exam891.Core.Bookings.Models;
using Exam891.Core.Bookings.Repositories;

namespace Exam891.Core.Tests.Bookings.Repositories
{
    public sealed class NaiveBookingsRepositoryTests
    {
        [Fact]
        public void GetDateRange_WhenNoBookings_ReturnsEmpty()
        {
            // Arrange
            var repository = new NaiveBookingsRepository();

            // Act
            var bookings = repository.GetDateRange("hotelId", "roomType", DateOnly.MinValue, DateOnly.MaxValue);

            // Assert
            Assert.Empty(bookings);
        }

        [Theory]
        [MemberData(nameof(GetDateRange_WhenBookings_ReturnsSingleBooking_CaseGenerator))]
        public void GetDateRange_WhenBookings_ReturnsSingleBooking(DateOnly from, DateOnly to)
        {
            // Arrange
            var repository = new NaiveBookingsRepository();
            repository.Add(new Booking
            {
                HotelId = "hotelId",
                Arrival = new DateOnly(2021, 1, 4),
                Departure = new DateOnly(2021, 1, 6),
                RoomType = "roomType",
                RoomRate = "roomRate"
            });

            // Act
            var bookings = repository.GetDateRange("hotelId", "roomType", from, to);

            // Assert
            Assert.Single(bookings);
        }

        public static TheoryData<DateOnly, DateOnly> GetDateRange_WhenBookings_ReturnsSingleBooking_CaseGenerator()
            => new()
            {
                { new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 5) }, // intersects arrival
                { new DateOnly(2021, 1, 5), new DateOnly(2021, 1, 9) }, // intersects departure
                { new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 9) }, // contains arrival and departure
            };
    }
}
