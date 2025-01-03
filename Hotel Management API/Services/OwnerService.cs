using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Data.DBContexts;
using Hotel_Management_API.DTOs.Extensions;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly HotelDBContext dBContext;
        public OwnerService(HotelDBContext dbContext)
        {
            this.dBContext = dbContext;
        }
        
        public async Task<List<OwnerResource>> GetAllOwnersAsync()
        {
            var owners = await dBContext.Owners.ToListAsync();
            return owners.ToOwnerResources();
        }

        public async Task<OwnerResource> GetOwnerAsync(long id)
        {
            var owner = await dBContext.Owners.FirstOrDefaultAsync(x => x.Id == id)
                         ?? throw new NotFoundException("Owner not found");
            return owner.ToOwnerResource();
        }

        public async Task<OwnerResource> ProcessPostOwnerRequest(PostOwnerRequest request)
        {
            var existingOwner = await dBContext.Owners.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingOwner is not null)
            {
                throw new DuplicateRequestException($"Duplicate request: Owner already exists");
            }
            var newOwner = request.ToOwner();
            var result = await dBContext.Owners.AddAsync(newOwner);
            await dBContext.SaveChangesAsync();
            return newOwner.ToOwnerResource();
        }

        public async Task<OwnerResource> ProcessPutOwnerRequest(long id, PutOwnerRequest request)
        {
            var existingOwner = await dBContext.Owners.FirstOrDefaultAsync(x => x.Id == id && x.Email == request.Email) 
                                ?? throw new BadRequestException($"Update owner failed: Owner does not exist");
            request.UpdateOwner(existingOwner);
            await dBContext.SaveChangesAsync();
            return request.ToOwnerResource(id);
        }
    }
}