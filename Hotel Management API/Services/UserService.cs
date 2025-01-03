using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Data.DBContexts;
using Hotel_Management_API.DTOs.Extensions;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Enums;
using Hotel_Management_API.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Services
{
    public class UserService : IUserService
    {
        private readonly HotelDBContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenService tokenService;
        public UserService(HotelDBContext dBContext, UserManager<User> userManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dBContext;
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.roleManager = roleManager;
        }

        public async Task<UserResource> GetUserResourceAsync(string userId)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId)
                                       ?? throw new NotFoundException($"User ${userId} not found");
            return user.ToUserResource();
        }

        public async Task<AuthResource> ProcessPostUserRequest(PostUserRequest request)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
            {
                var errMsg = "User already exists";
                throw new DuplicateRequestException(errMsg);
            }
            if (!await roleManager.RoleExistsAsync(request.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(request.Role));
            }
            User user = new()
            {
                UserName = request.FirstName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = (Role)Enum.Parse(typeof(Role), request.Role, true),
            };
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(error => error.Description));
                throw new BadRequestException(errors);
            }
            await userManager.AddToRoleAsync(user, request.Role.ToString());
            await userManager.UpdateAsync(user);
            var jwtToken = await this.tokenService.GenerateToken(user);
            return new AuthResource
            {
                Email = request.Email,
                Token = jwtToken,
                Roles = new List<string> { request.Role.ToString() },
                Message = "Registration successful",
                IsAuthenticated = true,
                ExpiresOn = tokenService.GetTokenExpirationTime(jwtToken)
            };
        }

        public async Task<AuthResource> ProcessLoginRequest(LoginUserRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
                throw new UnauthorizedAccessException("Email or Password is incorrect!");
            var jwtSecurityToken = await tokenService.GenerateToken(user);
            var rolesList = await userManager.GetRolesAsync(user);
            
            return new AuthResource
            {
                Email = request.Email,
                Token = jwtSecurityToken,
                Roles = rolesList.ToList(),
                Message = "Login successful",
                IsAuthenticated = true,
                ExpiresOn = tokenService.GetTokenExpirationTime(jwtSecurityToken)
            };
        }
    }
}