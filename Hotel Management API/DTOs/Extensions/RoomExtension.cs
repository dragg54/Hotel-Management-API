using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.DTOs.Resources.Data;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Enums;
using Hotel_Management_API.Models;

namespace Hotel_Management_API.DTOs.Extensions
{
    public static class RoomExtension
    {
        public static List<RoomResource> ToRoomResources(this List<Room> rooms)
        {
            if (rooms == null)
            {
                return new List<RoomResource>();
            }
            return rooms.Select(room => new RoomResource
            {
                Id = room.Id,

                CreatedAt = room.CreatedAt,
                UpdatedAt = room.UpdatedAt,
            }).ToList();
        }

        public static RoomResource ToRoomResource(this Room room)
        {
            return new RoomResource
            {
                Id = room.Id,
                Capacity = room.Capacity,               
                Number = room.Number,
                PricePerNight = room.PricePerNight,
                RoomClass = room.RoomClass.ToString(),
                CreatedAt = room.CreatedAt,
                UpdatedAt = room.UpdatedAt,
            };
        }

        public static Room ToRoom(this PostRoomRequest request)
        {
            Room room = new Room
            {
                Capacity = request.Capacity,
                HotelId = request.HotelId,
                Number = request.Number,
                PricePerNight = request.PricePerNight,
                RoomClass = (RoomType)Enum.Parse(typeof(RoomType), request.RoomClass, true)
            };
            return room;
        }

        public static RoomResource ToRoomResource(this PutRoomRequest request, long id)
        {
            return new RoomResource
            {
                Capacity = request.Capacity,
                Number = request.Number,
                PricePerNight = request.PricePerNight,
                RoomClass = request.RoomClass,
            };
        }

         public static RoomResource ToRoomResource(this PostRoomRequest request)
        {
            return new RoomResource
            {
                HotelId = request.HotelId,
                Capacity = request.Capacity,
                Number = request.Number,
                PricePerNight = request.PricePerNight,
                RoomClass = request.RoomClass,
            };
        }

        public static void UpdateRoom(this PutRoomRequest request, Room room)
        {
            room.Capacity = request.Capacity;
            room.Number = request.Number;
            room.PricePerNight = request.PricePerNight;
            room.RoomClass = (RoomType)Enum.Parse(typeof(RoomType), request.RoomClass, true);
            room.UpdatedAt = DateTime.Now;
        }

        public static Room ToRoom(this PutRoomRequest request)
        {
            return new Room
            {
                HotelId = request.HotelId,
                Capacity = request.Capacity,
                Number = request.Number,
                PricePerNight = request.PricePerNight,
                RoomClass = (RoomType)Enum.Parse(typeof(RoomType), request.RoomClass, true)
            };
        }
    }
}