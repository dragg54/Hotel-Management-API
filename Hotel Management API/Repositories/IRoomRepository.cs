using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Entities;

namespace Hotel_Management_API.Repositories
{
    public interface IRoomRepository
    {
        Task CreateRoomAsync(Room room);
        Task<Room> GetRoomAsync(long roomId);
        Task<List<Room>> GetAllRoomsAsync();
        Task UpdateRoomAsync(Room Room, Room existingRoom);
    }
}