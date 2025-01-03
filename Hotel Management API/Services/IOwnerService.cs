using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;

namespace Hotel_Management_API.Services
{
    public interface IOwnerService
    {
        Task<OwnerResource> ProcessPostOwnerRequest(PostOwnerRequest request);
        Task<OwnerResource> GetOwnerAsync(long id);
        Task<OwnerResource> ProcessPutOwnerRequest(long id, PutOwnerRequest request);  
        Task<List<OwnerResource>> GetAllOwnersAsync();
    }
}