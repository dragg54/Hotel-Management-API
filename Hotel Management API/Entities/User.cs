using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Hotel_Management_API.Enums;
using Microsoft.AspNetCore.Identity;

namespace Hotel_Management_API.Entities
{
    public class User: IdentityUser
    {
        public Role Role { get; set; }

        public string FirstName { get; set; }    

        public string LastName { get; set; }    
    }
}
