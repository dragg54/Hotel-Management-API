using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Models;

namespace Hotel_Management_API.Repositories
{
    public interface IHotelRepository
    {
        Task CreateHotelAsync(Hotel hotel);
        Task<Hotel> GetHotelAsync(long hotelId);
        Task<List<Hotel>> GetAllHotelsAsync();
        Task UpdateHotelAsync(Hotel hotel, Hotel existingHotel);
        Task<Hotel> GetHotelByOwnerIdAndNameAsync(long ownerId, string name);  
    }
}