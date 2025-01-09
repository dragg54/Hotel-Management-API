using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_API.DTOs.Requests
{
    public class PutRoomRequest
    {
        public long HotelId { get; set; }
        public int Capacity { get; set; }
        public string Number { get; set; }
        public decimal PricePerNight { get; set; }
        public string RoomClass { get; set; }
    }
}