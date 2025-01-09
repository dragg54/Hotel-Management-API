using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Repositories.Queries;

namespace Hotel_Management_API.Repositories
{
    public interface IOwnerRepository
    {
        Task CreateOwnerAsync(Owner owner);
        Task<Owner> GetOwnerAsync(long ownerId);
        Task<List<Owner>> GetAllOwnersAsync();
        Task UpdateOwnerAsync(Owner owner, Owner existingOwner);
        Task<Owner> GetOwnerByEmailAsync(string email);
    }
}