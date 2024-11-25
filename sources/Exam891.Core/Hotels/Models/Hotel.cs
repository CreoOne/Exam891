namespace Exam891.Core.Hotels.Models
{
    public class Hotel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public Roomtype[] RoomTypes { get; set; } = [];
        public Room[] Rooms { get; set; } = [];
    }
}
