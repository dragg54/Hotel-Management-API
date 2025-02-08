using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;

namespace Hotel_Management_API.Repositories
{
    public interface IBookingRepository
    {
        Task CreateBooking(Booking booking);
        Task<PaginatedList<Booking>> GetBookings(int pageSize, int pageNumber);
        Task<Booking> GetBooking(long id);
        Task<Booking> GetByUserIdAndRoomId(string userId, long roomId);  
    }
}
