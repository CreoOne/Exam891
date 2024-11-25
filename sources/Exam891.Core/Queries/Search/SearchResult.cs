namespace Exam891.Core.Queries.Search
{
    public readonly record struct SearchResult(DateOnly From, DateOnly To, int AvailableRoomsCount);
}
