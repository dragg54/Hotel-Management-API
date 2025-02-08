using Hotel_Management_API.Entities;
using Hotel_Management_API.Enums;

namespace Hotel_Management_API.DTOs.Requests
{
    public class PostBookingRequest
    {
        public long RoomId { get; set; }
        public string Status { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
