using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Enums;

namespace Hotel_Management_API.DTOs.Resources
{
    public class UserResource
    {
        public string Id { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }    
        public string Email { get; set; }
        public string UserName { get; set; }    
        public Role Role { get; set; }
    }
}