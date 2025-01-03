using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Enums;

namespace Hotel_Management_API.Entities
{
    public class Booking : BaseEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoomId { get; set; } 
        public BookingStatus Status {get;set;}
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public User User { get; set; }
        public Room Room { get; set; }
    }
}