using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Entities;

namespace Hotel_Management_API.Repositories.Extensions
{
    public static class RoomRepositoryExtension
    {
        public static void UpdateRoom(this Room room, Room existingRoom)
        {
            existingRoom.PricePerNight = room.PricePerNight;
            existingRoom.Capacity = room.Capacity;
            existingRoom.Number = room.Number;
            existingRoom.RoomClass = room.RoomClass;
            existingRoom.UpdatedAt =  room.UpdatedAt; 
        }    
    }
}