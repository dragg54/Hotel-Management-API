using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;

namespace Hotel_Management_API.DTOs.Extensions
{
    public static class UserExtension
    {
        public static UserResource ToUserResource(this User user)
        {
            return new UserResource
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Role = user.Role
            };
        }
    }
}