namespace Exam891.Core.Queries.Search
{
    internal readonly record struct SearchResult(DateOnly From, DateOnly To, int AvailableRoomsCount);
}
