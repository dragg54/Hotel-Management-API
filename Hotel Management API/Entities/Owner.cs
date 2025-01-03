using Hotel_Management_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Management_API.Entities
{
    public class Owner: BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone{ get; set; }

        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
