using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Entities;

namespace Hotel_Management_API.Repositories.Extensions
{
    public static class OwnerRepositoryExtension
    {
        public static void UpdateOwner(this Owner owner, Owner existingOwner)
        {
            existingOwner.Email = owner.Email;
            existingOwner.FirstName = owner.FirstName;  
            existingOwner.LastName = owner.LastName;    
            existingOwner.Phone = owner.Phone; 
        }
    }
}