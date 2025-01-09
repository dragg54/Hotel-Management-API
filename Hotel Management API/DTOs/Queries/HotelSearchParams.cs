using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_API.DTOs.Queries
{
    public class HotelSearchParams
    {
        public string Name { get; set;}
        public long OwnerId { get; set;}
    }
}