using Exam891.Core.Bookings.Models;

namespace Exam891.Core.Bookings.Repositories
{
    internal sealed class NaiveBookingsRepository : IBookingsRepository
    {
        private readonly List<Booking> _bookings = [];

        public void Add(Booking booking)
            => _bookings.Add(booking);

        public void Add(IEnumerable<Booking> bookings)
            => _bookings.AddRange(bookings);

        public IEnumerable<Booking> GetDateRange(string hotelId, string roomType, DateOnly from, DateOnly to)
            => _bookings
                .Where(booking => booking.HotelId == hotelId && booking.RoomType == roomType && booking.Departure >= from && booking.Arrival <= to);
    }
}
