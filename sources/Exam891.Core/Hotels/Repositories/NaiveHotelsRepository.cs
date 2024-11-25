using Exam891.Core.Hotels.Models;

namespace Exam891.Core.Hotels.Repositories
{
    internal sealed class NaiveHotelsRepository : IHotelsRepository
    {
        private readonly List<Hotel> _hotels = [];

        public void Add(Hotel hotel)
            => _hotels.Add(hotel);

        public void Add(IEnumerable<Hotel> hotels)
            => _hotels.AddRange(hotels);

        public int GetRoomCount(string hotelId, string roomType)
            => _hotels
                .FirstOrDefault(hotel => hotel.Id == hotelId)
                ?.Rooms
                .Count(room => room.RoomType == roomType) ?? 0;
    }
}
