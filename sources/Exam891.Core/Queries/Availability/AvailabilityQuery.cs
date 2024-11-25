using Exam891.Core.Queries.Search;

namespace Exam891.Core.Queries.Availability
{
    internal sealed class AvailabilityQuery
    {
        private readonly ISearchQuery _searchQuery;

        public AvailabilityQuery(ISearchQuery searchQuery)
            => _searchQuery = searchQuery;

        public int Availability(string hotelId, DateOnly date, string roomType)
            => Availability(hotelId, date, date, roomType);

        public int Availability(string hotelId, DateOnly from, DateOnly to, string roomType)
            => _searchQuery.Search(hotelId, from, to, roomType)
                .Select(result => result.AvailableRoomsCount)
                .DefaultIfEmpty()
                .Max();
    }
}
