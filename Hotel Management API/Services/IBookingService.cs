using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;

namespace Hotel_Management_API.Services
{
    public interface IBookingService
    {
        Task<BookingResource> ProcessPostBookingRequest(PostBookingRequest request, string userId);
        Task<BookingResource> GetBookingAsync(long id);
        Task<(int, List<BookingResource>)> GetBookingResourcesAsync(int pageSize, int pageNumber);
    }
}
