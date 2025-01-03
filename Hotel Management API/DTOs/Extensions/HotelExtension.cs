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
                Owner = new OwnerData
                {
                    OwnerId = hotel.Owner.Id,
                    FirstName = hotel.Owner.FirstName,
                    LastName = hotel.Owner.LastName,
                    Email = hotel.Owner.Email,  
                },
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
                Name = hotel.Name,
                Address = hotel.Address,
                StarRating = hotel.StarRating,
                State = hotel.State,
                City = hotel.City,
                Owner = new OwnerData
                {
                    OwnerId = hotel.Owner?.Id,
                    FirstName = hotel.Owner.FirstName,
                    LastName = hotel.Owner.LastName,
                    Email = hotel.Owner.Email
                },
                PostalCode = hotel.PostalCode,
                CreatedAt = hotel.CreatedAt,
                UpdatedAt = hotel.UpdatedAt,
            };
        }

        public static Hotel ToHotel(this PostHotelRequest request, Owner owner)
        {
            Hotel hotel = new Hotel
            {

                Name = request.Name,
                Address = request.Address,
                StarRating = request.StarRating,
                State = request.State,
                City = request.City,
                PostalCode = request.PostalCode,
                OwnerId = owner.Id,
                Owner = owner
            };
            return hotel;
        }

        public static HotelResource ToHotelResource(this PutHotelRequest request, long id, Owner owner)
        {
            return new HotelResource
            {
                Id = id,
                Name = request.Name,
                Address = request.Address,
                StarRating = request.StarRating,
                State = request.State,
                City = request.City,
                PostalCode = request.PostalCode,
                Owner = new OwnerData
                {
                    OwnerId = owner.Id,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Email = owner.Email
                }
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
    }
}