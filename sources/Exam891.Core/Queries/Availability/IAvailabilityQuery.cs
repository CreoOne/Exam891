
namespace Exam891.Core.Queries.Availability
{
    public interface IAvailabilityQuery
    {
        int Availability(string hotelId, DateOnly from, DateOnly to, string roomType);
    }
}