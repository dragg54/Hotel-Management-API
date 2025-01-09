using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Models;

namespace Hotel_Management_API.Repositories.Extensions
{
    public static class HotelRepositoryExtension
    {
        public static void UpdateHotel(this Hotel hotel, Hotel existingHotel)
        {
            existingHotel.Address = hotel.Address;  
            existingHotel.State = hotel.State;
            existingHotel.City = hotel.City;
            existingHotel.StarRating = hotel.StarRating;
            existingHotel.Country = hotel.Country;
            existingHotel.UpdatedAt = hotel.UpdatedAt;
        }
    }
}