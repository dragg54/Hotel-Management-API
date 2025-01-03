using FluentValidation;
using Hotel_Management_API.DTOs.Requests;

namespace Hotel_Management_API.DTOs.Validators
{
    public class PostOwnerRequestValidator : AbstractValidator<PostOwnerRequest>
    {
        public PostOwnerRequestValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("Last name is required.")
           .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

        }
    }
}
