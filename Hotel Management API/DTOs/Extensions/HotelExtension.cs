using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.DTOs.Resources.Data;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Models;

namespace Hotel_Management_API.DTOs.Extensions
{
    public static class HotelExtension
    {
        public static List<HotelResource> ToHotelResources(this List<Hotel> hotels)
        {
            if (hotels == null)
            {
                return new List<HotelResource>();
            }
            return hotels.Select(hotel => new HotelResource
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                OwnerId = hotel.OwnerId,
                StarRating = hotel.StarRating,
                State = hotel.State,
                City = hotel.City,
                PostalCode = hotel.PostalCode,
                CreatedAt = hotel.CreatedAt,
                UpdatedAt = hotel.UpdatedAt,
            }).ToList();
        }

        public static HotelResource ToHotelResource(this Hotel hotel)
        {
            return new HotelResource
            {
                Id = hotel.Id,
                OwnerId = hotel.Owner.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                StarRating = hotel.StarRating,
                State = hotel.State,
                City = hotel.City,
                PostalCode = hotel.PostalCode,
                CreatedAt = hotel.CreatedAt,
                UpdatedAt = hotel.UpdatedAt,
            };
        }

        public static Hotel ToHotel(this PostHotelRequest request)
        {
            Hotel hotel = new Hotel
            {
                Id = request.Id,
                Name = request.Name,
                Address = request.Address,
                StarRating = request.StarRating,
                State = request.State,
                City = request.City,
                PostalCode = request.PostalCode,
                OwnerId = request.OwnerId,
            };
            return hotel;
        }

         public static Hotel ToHotel(this PutHotelRequest request)
        {
            Hotel hotel = new Hotel
            {
                Name = request.Name,
                Address = request.Address,
                StarRating = request.StarRating,
                State = request.State,
                City = request.City,
                PostalCode = request.PostalCode,
            };
            return hotel;
        }

        public static HotelResource ToHotelResource(this PutHotelRequest request, long id, Hotel hotel)
        {
            return new HotelResource
            {
                Id = id,
                OwnerId = hotel.Owner.Id,
                Name = request.Name,
                Address = request.Address,
                StarRating = request.StarRating,
                State = request.State,
                City = request.City,
                PostalCode = request.PostalCode,
            };
        }

        public static void UpdateHotel(this PutHotelRequest request, Hotel hotel)
        {
            hotel.Name = request.Name;
            hotel.Address = request.Address;
            hotel.StarRating = request.StarRating;
            hotel.State = request.State;
            hotel.City = request.City;
            hotel.PostalCode = request.PostalCode;
            hotel.UpdatedAt = DateTime.Now;
        }

        public static Hotel ToHotel(this HotelResource resource)
        {
            return new Hotel
            {
                
                OwnerId = resource.OwnerId,
                Name = resource.Name,
                Address = resource.Address,
                StarRating = resource.StarRating,
                State = resource.State,
                City = resource.City,
                PostalCode = resource.PostalCode,
            };
        }
    }
}