using Hotel_Management_API.Enums;

namespace Hotel_Management_API.Repositories.Queries
{
    public class RoomSearchQuery
    {
        public long HotelId { get; set; }
        public int Capacity { get; set; }
        public string? Number { get; set; }
        public decimal PricePerNight { get; set; }
        public string? RoomClass { get; set; }
        public string? BookingStatus { get; set; }
    }
}
