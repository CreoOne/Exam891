using Exam891.Core.Hotels.Models;

namespace Exam891.Core.Hotels.Repositories
{
    public interface IHotelsRepository
    {
        void Add(Hotel hotel);
        void Add(IEnumerable<Hotel> hotels);
        int GetRoomCount(string hotelId, string roomType);
    }
}