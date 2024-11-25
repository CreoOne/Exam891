using Exam891.Core.Queries.Search;

namespace Exam891.Core.Queries.Availability
{
    public sealed class AvailabilityQuery : IAvailabilityQuery
    {
        private readonly ISearchQuery _searchQuery;

        public AvailabilityQuery(ISearchQuery searchQuery)
            => _searchQuery = searchQuery;

        public int Availability(string hotelId, DateOnly from, DateOnly to, string roomType)
            => _searchQuery.Search(hotelId, from, to, roomType)
                .Select(result => result.AvailableRoomsCount)
                .DefaultIfEmpty()
                .Max();
    }
}
