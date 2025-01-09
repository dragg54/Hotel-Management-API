using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Resources.Data;

namespace Hotel_Management_API.DTOs.Resources
{
    public class RoomResource
    {
        public long Id { get; set; }  
        public long HotelId { get; set; }  
        public int Capacity { get; set; }
        public string Number { get; set; }
        public decimal PricePerNight { get; set; }
        public string RoomClass { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
    }
}