using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Data.DBContexts;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Models;
using Hotel_Management_API.Repositories.Extensions;
using Hotel_Management_API.Repositories.Queries;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Repositories
{
    public class HotelRepository: IHotelRepository
    {
        private readonly HotelDBContext _dbContext;
        public HotelRepository(HotelDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task CreateHotelAsync(Hotel hotel)
        {
            await _dbContext.Hotels.AddAsync(hotel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<Hotel>> GetAllHotelsAsync(HotelSearchQuery query, int pageSize, int pageNumber = 1)
        {
            IQueryable<Hotel> hotels = _dbContext.Hotels;
            if(query.Name != null)
            {
                hotels = hotels.Where(hotel => hotel.Name == query.Name);
            }
            if (query.City != null)
            {
                hotels = hotels.Where(hotel => hotel.City == query.City);
            }
            if(query.Address != null)
            {
                hotels = hotels.Where(hotel => EF.Functions.Like(hotel.Address, query.Address));
            }
            var paginatedResult = await PaginatedList<Hotel>.CreateAsync(hotels.AsNoTracking(), pageNumber, pageSize);
            return paginatedResult;
        }

        public async Task<Hotel> GetHotelAsync(long hotelId)
        {
            return await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
        }

         public async Task<Hotel> GetHotelByOwnerIdAndNameAsync(long hotelId, string name)
        {
            return await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId & h.Name == name);
        }

        public async Task UpdateHotelAsync(Hotel hotel, Hotel existingHotel)
        {
            hotel.UpdateHotel(existingHotel);
            await _dbContext.SaveChangesAsync();
        }
    }
}