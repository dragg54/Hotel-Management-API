using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Extensions;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Exceptions;
using Hotel_Management_API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository ownerRepository;
        public OwnerService(IOwnerRepository ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }
        
        public async Task<List<OwnerResource>> GetAllOwnersAsync()
        {
            var owners = await ownerRepository.GetAllOwnersAsync();
            return owners.ToOwnerResources();
        }

        public async Task<OwnerResource> GetOwnerAsync(long id)
        {
            var owner = await ownerRepository.GetOwnerAsync(id)
                         ?? throw new NotFoundException("Owner not found");
            return owner.ToOwnerResource();
        }

        public async Task<OwnerResource> ProcessPostOwnerRequest(PostOwnerRequest request)
        {
            var existingOwner = await ownerRepository.GetOwnerByEmailAsync(request.Email);
            if (existingOwner is not null)
            {
                throw new DuplicateRequestException($"Duplicate request: Owner already exists");
            }
            var newOwner = request.ToOwner();
            await ownerRepository.CreateOwnerAsync(newOwner);
            return newOwner.ToOwnerResource();
        }

        public async Task<OwnerResource> ProcessPutOwnerRequest(long id, PutOwnerRequest request)
        {
            var existingOwner = await ownerRepository.GetOwnerAsync(id) 
                                ?? throw new BadRequestException($"Update owner failed: Owner does not exist");
            return request.ToOwnerResource(id);
        }
    }
}