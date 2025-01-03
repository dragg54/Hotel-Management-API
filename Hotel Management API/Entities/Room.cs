using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Enums;
using Hotel_Management_API.Models;

namespace Hotel_Management_API.Entities
{
    public class Room: BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public int Capacity{get;set;}
        public string Number{get;set;}
        public decimal PricePerNight {get;set;}
        public RoomType RoomClass {get;set;}

        public Hotel Hotel {get;set;}
        public ICollection<Booking> Bookings {get;set;}
    }
}