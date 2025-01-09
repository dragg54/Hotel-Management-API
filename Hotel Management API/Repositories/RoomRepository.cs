using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Data.DBContexts;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Models;
using Hotel_Management_API.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDBContext _dbContext;
        public RoomRepository(HotelDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task CreateRoomAsync(Room room)
        {

            await _dbContext.Rooms.AddAsync(room);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _dbContext.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomAsync(long RoomId)
        {
            return await _dbContext.Rooms.FirstOrDefaultAsync(x => x.Id == RoomId);
        }

        public async Task UpdateRoomAsync(Room room, Room existingRoom)
        {
            room.UpdateRoom(existingRoom);
            await _dbContext.SaveChangesAsync();
        }
    }
}