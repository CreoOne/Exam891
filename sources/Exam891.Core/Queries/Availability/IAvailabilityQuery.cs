
namespace Exam891.Core.Queries.Availability
{
    internal interface IAvailabilityQuery
    {
        int Availability(string hotelId, DateOnly from, DateOnly to, string roomType);
        int Availability(string hotelId, DateOnly date, string roomType);
    }
}