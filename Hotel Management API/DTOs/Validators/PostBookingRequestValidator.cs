using FluentValidation;
using Hotel_Management_API.DTOs.Requests;

namespace Hotel_Management_API.DTOs.Validators
{
    public class PostBookingRequestValidator : AbstractValidator<PostBookingRequest>
    {
        public PostBookingRequestValidator()
        {
            RuleFor(x => x.RoomId)
           .NotEmpty().WithMessage("RooId is required.");

            RuleFor(x => x.CheckInDate)
            .NotEmpty().WithMessage("Check in date is required.");

            RuleFor(x => x.CheckOutDate)
            .NotEmpty().WithMessage("Checkout date is required.");
        }
    }
}
