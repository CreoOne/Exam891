
namespace Exam891.Core.Queries.Search
{
    internal interface ISearchQuery
    {
        IEnumerable<SearchResult> Search(string hotelId, DateOnly startDate, DateOnly endDate, string roomType);
    }
}