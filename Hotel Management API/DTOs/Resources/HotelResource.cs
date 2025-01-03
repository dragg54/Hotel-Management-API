using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Resources.Data;
using Hotel_Management_API.Entities;

namespace Hotel_Management_API.DTOs.Resources
{
    public class HotelResource
    {

        public long Id { get; set; }

        public OwnerData Owner { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public int StarRating { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}