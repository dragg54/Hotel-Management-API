using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Entities;

namespace Hotel_Management_API.Services
{
    public interface ITokenService
    {
       Task<string> GenerateToken(User user); 
       DateTime? GetTokenExpirationTime(string jwtSecurityToken);
    }
}