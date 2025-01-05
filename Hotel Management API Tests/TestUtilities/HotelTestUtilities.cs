using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;

namespace Hotel_Management_API_Tests.TestUtilities
{
    public class HotelTestUtilities
    {
        public static PostHotelRequest SetupPostHotelRequest()
        {
            return new PostHotelRequest
            {
                Name = "Binks Hotel",
                Address = "No 32, Alakia Street, Akala Road",
                City = "Ibadan",
                PostalCode = "24948",
                OwnerId = 2L,
                StarRating = 4,
                State = "Oyo",
            };
        }

        public static PutHotelRequest SetupPutHotelRequest()
        {
            return new PutHotelRequest
            {
                Name = "Binks Hotel",
                Address = "No 32, Alakia Street, Akala Road",
                City = "Ibadan",
                PostalCode = "24948",
                StarRating = 4,
                State = "Oyo",
            };
        }
    }
}