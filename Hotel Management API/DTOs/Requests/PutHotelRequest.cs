using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_API.DTOs.Requests
{
    public class PutHotelRequest
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City {get;set;}

        public string State { get; set; }  

        public string PostalCode {get; set; } 

        public int StarRating {get;set;}
    }
}