using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;

namespace Hotel_Management_API.Services
{
    public interface IRoomService
    {
        Task<RoomResource> ProcessPostRoomRequest(PostRoomRequest request);
        Task<RoomResource> ProcessPutRoomRequest(long id, PutRoomRequest request); 
        Task<RoomResource> GetRoomAsync(long id);
        Task<List<RoomResource>> GetRoomsAsync();  
    }
}