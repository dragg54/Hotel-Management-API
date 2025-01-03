using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;

namespace Hotel_Management_API.Services
{
    public interface IHotelService
    {
        Task<HotelResource> ProcessPostHotelRequest(PostHotelRequest request);
        Task<List<HotelResource>> GetHotelsAsync();
        Task<HotelResource> ProcessPutHotelRequest(PutHotelRequest request, long id);
        Task<HotelResource> GetHotelAsync(long id);
    }
}