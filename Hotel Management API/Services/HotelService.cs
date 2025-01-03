using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Data.DBContexts;
using Hotel_Management_API.DTOs.Extensions;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Exceptions;
using Hotel_Management_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Services
{
    public class HotelService : IHotelService
    {
        private readonly HotelDBContext _dbContext;
        public HotelService(HotelDBContext hotelDBContext)
        {
            _dbContext = hotelDBContext;
        }

        public async Task<HotelResource> ProcessPostHotelRequest(PostHotelRequest request)
        {
            var owner = await _dbContext.Owners.FindAsync(request.OwnerId);
            if (owner == null)
            {
                throw new NotFoundException("Owner not found");
            }
            var existingHotel = await _dbContext.Hotels.FirstOrDefaultAsync(hot =>
                hot.Name == request.Name && owner.Id == request.OwnerId
              );
            if (existingHotel != null)
            {
                throw new DuplicateRequestException("Hotel already exists");
            }
            var newHotel = request.ToHotel(owner);
            newHotel.Owner = owner ?? throw new NotFoundException("Owner not found.");
            await _dbContext.Hotels.AddAsync(newHotel);
            await _dbContext.SaveChangesAsync();
            return newHotel.ToHotelResource();
        }

        public async Task<List<HotelResource>> GetHotelsAsync()
        {
            var hotels = await _dbContext.Hotels
                               .Include(o => o.Owner)
                               .ToListAsync();
            return hotels.ToHotelResources();
        }

        public async Task<HotelResource> ProcessPutHotelRequest(PutHotelRequest request, long id)
        {
            var existingHotel = await _dbContext.Hotels
                                 .Include(h => h.Owner)
                                 .FirstOrDefaultAsync(ho => ho.Id == id)
                                ?? throw new NotFoundException("Hotel not found");
            request.UpdateHotel(existingHotel);
            await _dbContext.SaveChangesAsync();
            return request.ToHotelResource(id, existingHotel.Owner);
        }

        public async Task<HotelResource> GetHotelAsync(long id)
        {
            var hotel = await _dbContext.Hotels
                                           .Include(o => o.Owner)
                                           .FirstOrDefaultAsync(ho => ho.Id == id);
            return hotel.ToHotelResource();
        }
    }
}