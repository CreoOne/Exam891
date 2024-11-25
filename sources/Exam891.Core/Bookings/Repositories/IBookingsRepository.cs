using Exam891.Core.Bookings.Models;

namespace Exam891.Core.Bookings.Repositories
{
    internal interface IBookingsRepository
    {
        void Add(Booking booking);
        void Add(IEnumerable<Booking> bookings);
        IEnumerable<Booking> GetDateRange(string hotelId, string roomType, DateOnly from, DateOnly to);
    }
}