using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;

namespace Hotel_Management_API.Services
{
    public interface IUserService
    {
        Task<AuthResource> ProcessPostUserRequest(PostUserRequest request);
        Task<AuthResource> ProcessLoginRequest(LoginUserRequest request);
        Task<UserResource> GetUserResourceAsync(string userId); 
    }
}