using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;

namespace Hotel_Management_API_Tests.TestUtilities
{
    public static class OwnerTestUtilities
    {
        public static PostOwnerRequest SetupPostOwnerRequest()
        {
            return new PostOwnerRequest
            {
                FirstName = "Akinyemi",
                LastName = "Alao",
                Email = "akinyemi@yahoo.com",
            };
        }

        public static PutOwnerRequest SetupPutOwnerRequest()
        {
            return new PutOwnerRequest
            {
                FirstName = "Akinyemi",
                LastName = "Alao",
                Email = "akinyemi@yahoo.com",
            };
        }
    }
}