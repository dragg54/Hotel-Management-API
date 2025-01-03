using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Management_API.Entities
{
    public class Address
    {
        public long Id { get; set; }    
        public string Street { get; set; }
        public string City { get; set; }    
        public string State { get; set; }   
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
