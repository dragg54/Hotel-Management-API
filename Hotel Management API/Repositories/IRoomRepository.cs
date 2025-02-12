using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Repositories.Queries;

namespace Hotel_Management_API.Repositories
{
    public interface IRoomRepository
    {
        Task CreateRoomAsync(Room room);
        Task<Room> GetRoomAsync(long roomId);
        Task<PaginatedList<Room>> GetAllRoomsAsync(RoomSearchQuery query ,int pageSize, int pageNumber);
        Task UpdateRoomAsync(Room Room, Room existingRoom);
    }
}