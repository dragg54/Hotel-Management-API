using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Models;

namespace Hotel_Management_API.DTOs.Resources
{
    public class OwnerResource
    {
        public long Id { get; set; }    
         public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<Hotel> Hotels { get; set; }  

        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; } 

        public DateTime UpdatedAt { get; set; } 
    }
}