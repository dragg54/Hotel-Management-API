using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Extensions;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Exceptions;
using Hotel_Management_API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Services
{
    public class RoomService : IRoomService
    {
         private readonly IRoomRepository _roomRepository;
         private readonly IHotelRepository _hotelRepository;
        public RoomService(IRoomRepository roomRepository, IHotelRepository hotelRepository)
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
        }
        public async Task<RoomResource> GetRoomAsync(long id)
        {
            var room = await _roomRepository.GetRoomAsync(id)
                        ?? throw new NotFoundException("Room not found"); 
            return room.ToRoomResource();
        }

        public async Task<List<RoomResource>> GetRoomsAsync()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();
            return rooms.ToRoomResources();
        }

        public async Task<RoomResource> ProcessPostRoomRequest(PostRoomRequest request)
        {
            var existingHotel = await _hotelRepository.GetHotelAsync(request.HotelId)
                              ?? throw new BadRequestException("Failed to create hotel: Hotel must exist before a room can be added");

            Room room = request.ToRoom();
            await _roomRepository.CreateRoomAsync(room);
            return request.ToRoomResource(room.Id);
        }

        public async Task<RoomResource> ProcessPutRoomRequest(long id, PutRoomRequest request)
        {
            var existingRoom = await _roomRepository.GetRoomAsync(id)
                        ?? throw new NotFoundException("Room not found"); 
            var newRoom = request.ToRoom();
            await _roomRepository.UpdateRoomAsync(newRoom, existingRoom);
            return request.ToRoomResource(id);
        }
    }
}