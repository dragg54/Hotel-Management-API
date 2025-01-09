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
using Hotel_Management_API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IOwnerRepository ownerRepository;    
        public HotelService(IOwnerRepository ownerRepository, IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
            this.ownerRepository = ownerRepository;
        }

        public async Task<HotelResource> ProcessPostHotelRequest(PostHotelRequest request)
        {
            var owner = ownerRepository.GetOwnerAsync(request.OwnerId);
            var existingHotel = await hotelRepository.GetHotelByOwnerIdAndNameAsync(owner.Id, request.Name);
            if (existingHotel != null)
            {
                throw new DuplicateRequestException("Hotel already exists");
            }
            var newHotel = request.ToHotel();
            await hotelRepository.CreateHotelAsync(newHotel);
            return newHotel.ToHotelResource();
        }

        public async Task<List<HotelResource>> GetHotelsAsync()
        {
            var hotels = await hotelRepository.GetAllHotelsAsync();
            return hotels.ToHotelResources();
        }

        public async Task<HotelResource> ProcessPutHotelRequest(PutHotelRequest request, long id)
        {
            var existingHotel = await hotelRepository.GetHotelAsync(id);
            request.UpdateHotel(existingHotel);
            await hotelRepository.UpdateHotelAsync(request.ToHotel(), existingHotel);
            return request.ToHotelResource(id, existingHotel);
        }

        public async Task<HotelResource> GetHotelAsync(long id)
        {
            var hotel = await hotelRepository.GetHotelAsync(id)
                                            ?? throw new NotFoundException("Hotel not found");
            return hotel.ToHotelResource();
        }
    }
}