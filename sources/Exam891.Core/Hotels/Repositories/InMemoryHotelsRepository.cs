using Exam891.Core.Hotels.Models;

namespace Exam891.Core.Hotels.Repositories
{
    public sealed class InMemoryHotelsRepository : IHotelsRepository
    {
        private readonly Dictionary<string, Hotel> _hotels = [];

        public void Add(Hotel hotel)
            => _hotels.Add(hotel.Id, hotel);

        public void Add(IEnumerable<Hotel> hotels)
        {
            foreach (var hotel in hotels)
                Add(hotel);
        }

        public int GetRoomCount(string hotelId, string roomType)
        {
            if (!_hotels.TryGetValue(hotelId, out var hotel))
                return 0;

            if (hotel.Rooms.Length == 0)
                return 0;

            return hotel.Rooms.Count(room => room.RoomType == roomType);
        }
    }
}
