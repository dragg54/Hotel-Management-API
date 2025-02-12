using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Repositories.Queries;

namespace Hotel_Management_API.Services
{
    public interface IHotelService
    {
        Task<HotelResource> ProcessPostHotelRequest(PostHotelRequest request);
        Task<(int count, List<HotelResource> data)> GetHotelsAsync(HotelSearchQuery query, int pageSize, int pageNumber);
        Task<HotelResource> ProcessPutHotelRequest(PutHotelRequest request, long id);
        Task<HotelResource> GetHotelAsync(long id);
    }
}