using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_API.DTOs.Resources.Data
{
    public class OwnerData
    {
        public long? OwnerId { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set;} 
        public string Email { get; set; }   
    }
}