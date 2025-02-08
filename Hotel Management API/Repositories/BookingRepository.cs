using Hotel_Management_API.Data.DBContexts;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Repositories
{
    public class BookingRepository: IBookingRepository
    {
        private readonly HotelDBContext _dbContext;
        public BookingRepository(HotelDBContext dbContext)
        {
            _dbContext= dbContext;  
        }

        public async Task CreateBooking(Booking booking)
        {   
            await _dbContext.Bookings.AddAsync(booking);    
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Booking> GetBooking(long id)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            return booking;
        }

        public async Task<PaginatedList<Booking>> GetBookings(int pageSize, int pageNumber = 1)
        {
           var bookings = _dbContext.Bookings;
           var paginatedBookings = await PaginatedList<Booking>.CreateAsync(bookings, pageNumber, pageSize);
           return paginatedBookings;
        }

        public async Task<Booking> GetByUserIdAndRoomId(string userId, long roomId)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.UserId == userId && x.RoomId == roomId);
            return booking;
        }
    }
}
