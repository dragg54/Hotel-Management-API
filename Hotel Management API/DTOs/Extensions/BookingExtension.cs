using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Enums;

namespace Hotel_Management_API.DTOs.Extensions
{
    public static class BookingExtension
    {
        public static List<BookingResource> ToBookingResources(this List<Booking> bookings)
        {
            if (bookings == null)
            {
                return new List<BookingResource>();
            }
            return bookings.Select(booking => new BookingResource
            {
                Id = booking.Id,
                UserId = booking.UserId,
                RoomId = booking.RoomId,
                Status = booking.Status.ToString(),
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                CreatedAt = booking.CreatedAt,
                UpdatedAt = booking.UpdatedAt,
            }).ToList();
        }

        public static BookingResource ToBookingResource(this Booking booking)
        {
            return new BookingResource
            {
                Id = booking.Id,
                UserId = booking.UserId,
                RoomId = booking.RoomId,
                Status = booking.Status.ToString(),
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                CreatedAt = booking.CreatedAt,
                UpdatedAt = booking.UpdatedAt,
            };
        }

        public static Booking ToBooking(this PostBookingRequest request, string userId)
        {
            Booking booking = new Booking
            {
                UserId = userId,
                RoomId = request.RoomId,
                Status = (BookingStatus)Enum.Parse(typeof(BookingStatus), request.Status, true),
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
            };
            return booking;
        }

        //public static BookingResource ToBookingResource(this PutBookingRequest request, long id)
        //{
        //    return new BookingResource
        //    {
        //        Capacity = request.Capacity,
        //        Number = request.Number,
        //        PricePerNight = request.PricePerNight,
        //        BookingClass = request.BookingClass,
        //    };
        //}


        //public static void UpdateBooking(this PutBookingRequest request, Booking Booking)
        //{
        //    Booking.Capacity = request.Capacity;
        //    Booking.Number = request.Number;
        //    Booking.PricePerNight = request.PricePerNight;
        //    Booking.BookingClass = (BookingType)Enum.Parse(typeof(RoomType), request.RoomClass, true);
        //    room.UpdatedAt = DateTime.Now;
        //}
    }
}
