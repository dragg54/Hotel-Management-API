using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_API.Repositories.Queries
{
    public class HotelSearchQuery
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
    }
}