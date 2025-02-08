using Hotel_Management_API.DTOs.Queries;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Responses;
using Hotel_Management_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Management_API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController: ControllerBase
    {
        private readonly IBookingService bookingService;
        private readonly IUserService userService;
        private readonly IResponseHandler responseHandler;
        public BookingController(IBookingService bookingService, IUserService userService, IResponseHandler responseHandler)
        {
            this.bookingService = bookingService;
            this.userService = userService;
            this.responseHandler = responseHandler;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookingResource))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostBookingAsync([FromBody] PostBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userService.GetLoggedInUserAsync();
            var result = await bookingService.ProcessPostBookingRequest(request, user.Id);
            return responseHandler.Created(result, "Booking created successful");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookingResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookings([FromQuery] PaginationQuery query)
        {
            var result = await bookingService.GetBookingResourcesAsync(query.pageSize, query.pageNumber);
            return responseHandler.Success(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookingResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooking(long id)
        {
            var result = await bookingService.GetBookingAsync(id);
            return responseHandler.Success(result);
        }
    }
}
