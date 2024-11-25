using Exam891.Core.Bookings.Repositories;
using Exam891.Core.Hotels.Repositories;

namespace Exam891.Core.Queries.Search
{
    internal sealed class SearchQuery : ISearchQuery
    {
        private readonly IHotelsRepository _hotelsRepository;
        private readonly IBookingsRepository _bookingsRepository;

        public SearchQuery(IHotelsRepository hotelsRepository, IBookingsRepository bookingsRepository)
        {
            _hotelsRepository = hotelsRepository;
            _bookingsRepository = bookingsRepository;
        }

        public IEnumerable<SearchResult> Search(string hotelId, DateOnly startDate, DateOnly endDate, string roomType)
        {
            var roomCount = _hotelsRepository.GetRoomCount(hotelId, roomType);
            var bookings = _bookingsRepository.GetDateRange(hotelId, roomType, startDate, endDate);

            if (!bookings.Any())
            {
                yield return new SearchResult(startDate, endDate, roomCount);
                yield break;
            }

            var allDates = bookings
                .SelectMany(booking => new DateOnly[] { booking.Arrival, booking.Departure })
                .Distinct()
                .OrderBy(date => date);

            foreach (var date in allDates)
            {
                // TODO: PERF HIT - limit number of .Count() calls
                var availableRooms = roomCount - bookings.Count(booking => booking.Arrival <= startDate && booking.Departure >= date);
                yield return new SearchResult(startDate, date, availableRooms);
                startDate = date;
            }

            if (startDate != endDate)
                yield return new SearchResult(startDate, endDate, roomCount);
        }
    }
}
