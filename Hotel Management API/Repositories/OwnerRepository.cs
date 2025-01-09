using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Data.DBContexts;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Exceptions;
using Hotel_Management_API.Repositories.Extensions;
using Hotel_Management_API.Repositories.Queries;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly HotelDBContext _dbContext;
        public OwnerRepository(HotelDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateOwnerAsync(Owner owner)
        {

            await _dbContext.Owners.AddAsync(owner);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Owner>> GetAllOwnersAsync()
        {
             return await _dbContext.Owners.ToListAsync();
        }

        public async Task<Owner> GetOwnerAsync(long ownerId)
        {
            return await _dbContext.Owners.FirstOrDefaultAsync(x => x.Id == ownerId);
        }


        public async Task<Owner> GetOwnerByEmailAsync(string email)
        {
            return await _dbContext.Owners.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateOwnerAsync(Owner owner, Owner existingOwner)
        {
            owner.UpdateOwner(existingOwner);
            await _dbContext.SaveChangesAsync();
        }
    }
}