using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Models;
using Hotel_Management_API.Repositories.Queries;

namespace Hotel_Management_API.Repositories
{
    public interface IHotelRepository
    {
        Task CreateHotelAsync(Hotel hotel);
        Task<Hotel> GetHotelAsync(long hotelId);
        Task<PaginatedList<Hotel>> GetAllHotelsAsync(HotelSearchQuery query, int pageSize , int pageNumber);
        Task UpdateHotelAsync(Hotel hotel, Hotel existingHotel);
        Task<Hotel> GetHotelByOwnerIdAndNameAsync(long ownerId, string name);  
    }
}