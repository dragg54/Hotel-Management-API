using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;

namespace Hotel_Management_API.DTOs.Extensions
{
    public static class OwnerExtension
    {
        public static List<OwnerResource> ToOwnerResources(this List<Owner> owners)
        {
            return owners.Select(owner => new OwnerResource
            {
                Id = owner.Id,
                Email = owner.Email,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Hotels = owner.Hotels,
                CreatedAt = owner.CreatedAt,
                UpdatedAt = owner.UpdatedAt,
            }).ToList();
        }

        public static OwnerResource ToOwnerResource(this Owner owner)
        {
            return new OwnerResource
            {
                Id = owner.Id,
                Email = owner.Email,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Hotels = owner.Hotels,
                CreatedAt = owner.CreatedAt,
                UpdatedAt = owner.UpdatedAt,
            };
        }

        public static Owner ToOwner(this PostOwnerRequest request)
        {
            return new Owner
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
        }

        public static OwnerResource ToOwnerResource(this PutOwnerRequest request, long id)
        {
            return new OwnerResource
            {
                Id = id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
        }

        public static void UpdateOwner(this PutOwnerRequest request, Owner owner)
        {
            owner.FirstName = request.FirstName;
            owner.LastName = request.LastName;
            owner.Email = request.Email;    
        }
    }
}