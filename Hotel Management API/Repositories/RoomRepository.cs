using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Data.DBContexts;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Models;
using Hotel_Management_API.Repositories.Extensions;
using Hotel_Management_API.Repositories.Queries;
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

        public async Task<PaginatedList<Room>> GetAllRoomsAsync(RoomSearchQuery query, int pageSize, int pageNumber = 1)
        {
            IQueryable<Room> rooms = _dbContext.Rooms;
            if (query.Capacity != 0)
            {
                rooms = rooms.Where(room => room.Capacity == query.Capacity);
            }
            if (query.RoomClass != null)
            {
                rooms = rooms.Where(room => room.RoomClass.ToString() == query.RoomClass);
            }
            if (query.Number != null)
            {
                rooms = rooms.Where(room => EF.Functions.Like(room.Number, query.Number));
            }
            if(query.BookingStatus != null)
            {
                rooms = rooms.Where(room => (room.Bookings.LastOrDefault().Status.ToString()) == query.BookingStatus);
             }
            var paginateRoom = await PaginatedList<Room>.CreateAsync(rooms.AsNoTracking(), pageNumber, pageSize);
            return paginateRoom;
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