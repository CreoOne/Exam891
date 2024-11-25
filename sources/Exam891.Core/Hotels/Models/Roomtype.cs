namespace Exam891.Core.Hotels.Models
{
    public class Roomtype
    {
        public required string Code { get; set; }
        public string Description { get; set; } = string.Empty;
        public string[] Amenities { get; set; } = [];
        public string[] Features { get; set; } = [];
    }

}
