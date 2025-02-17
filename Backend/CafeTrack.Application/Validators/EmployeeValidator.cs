using FluentValidation;
using CafeTrack.Application.DTOs;

namespace CafeTrack.Application.Validators
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.EmailAddress)
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^[89]\d{7}$").WithMessage("Phone number must start with 8 or 9 and contain 8 digits.");

            RuleFor(x => x.Gender)
                .Must(g => g == "Male" || g == "Female").WithMessage("Gender must be either 'Male' or 'Female'.");
        }
    }
}
