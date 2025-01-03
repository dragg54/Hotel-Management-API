using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Hotel_Management_API.DTOs.Requests;

namespace Hotel_Management_API.DTOs.Validators
{
    public class PostHotelRequestValidator : AbstractValidator<PostHotelRequest>
    {
        public PostHotelRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(x => x.Address)
           .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.");

            RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner id is required.");

            RuleFor(x => x.City)
           .NotEmpty().WithMessage("cITY is required.");

            RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal code is required.");

            RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal code is required.");

            RuleFor(x => x.StarRating)
            .NotEmpty().WithMessage("Star rating is required.")
            .LessThanOrEqualTo(5)
            .GreaterThan(0);
        }
    }
}
