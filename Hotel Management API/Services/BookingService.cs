using Hotel_Management_API.DTOs.Extensions;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Enums;
using Hotel_Management_API.Exceptions;
using Hotel_Management_API.Repositories;

namespace Hotel_Management_API.Services
{
    public class BookingService: IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IRoomRepository roomRepository;
        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            this.bookingRepository = bookingRepository; 
            this.roomRepository= roomRepository;    
        }

        public async Task<BookingResource> GetBookingAsync(long id)
        {
            var booking = await bookingRepository.GetBooking(id);
            return booking.ToBookingResource();
        }

        public async Task<(int, List<BookingResource>)> GetBookingResourcesAsync(int pageSize, int pageNumber)
        {
            var bookings = await bookingRepository.GetBookings(pageSize, pageNumber);
            return (bookings.TotalPages, bookings.ToList().ToBookingResources());
        }

        public async Task<BookingResource> ProcessPostBookingRequest(PostBookingRequest request, string userId)
        {
            var existingBooking = await bookingRepository.GetByUserIdAndRoomId(userId, request.RoomId);
            if(existingBooking != null)
            {
                throw new DuplicateRequestException($"Booking by user {userId} already exists");
            }
            var existingRoom = await roomRepository.GetRoomAsync(request.RoomId);   
            if(existingRoom == null || existingBooking.Status == BookingStatus.Accepted)
            {
                throw new BadRequestException($"Room does not exist or has already been booked");
            }
            var booking = request.ToBooking(userId);
            await bookingRepository.CreateBooking(booking);
            return booking.ToBookingResource();    
        }
    }
}
