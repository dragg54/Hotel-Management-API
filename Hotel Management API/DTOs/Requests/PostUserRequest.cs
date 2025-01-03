using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Enums;

namespace Hotel_Management_API.DTOs.Requests
{
    public class PostUserRequest
    {
        [EmailAddress]
        public string Email { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }    
        public string Role { get; set; }
        public string Password { get; set; }    
    }
}