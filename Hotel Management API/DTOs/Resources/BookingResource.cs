namespace Hotel_Management_API.DTOs.Resources
{
    public class BookingResource
    {
        public long Id { get; set; }    
        public string UserId { get; set; }
        public long RoomId { get; set; }
        public string Status { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
