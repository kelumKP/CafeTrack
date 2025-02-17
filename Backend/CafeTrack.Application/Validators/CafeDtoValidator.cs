using CafeTrack.Application.DTOs;
using FluentValidation;

namespace CafeTrack.Application.Validators
{
    public class CafeDtoValidator : AbstractValidator<CafeDto>
    {
        public CafeDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(c => c.Location).NotEmpty().WithMessage("Location is required.");
            // Add other validation rules here
        }
    }
}
