
namespace Exam891.Core.Queries.Search
{
    public interface ISearchQuery
    {
        IEnumerable<SearchResult> Search(string hotelId, DateOnly startDate, DateOnly endDate, string roomType);
    }
}