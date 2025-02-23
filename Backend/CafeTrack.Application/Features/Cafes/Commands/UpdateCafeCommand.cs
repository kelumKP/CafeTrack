using MediatR;
using Microsoft.AspNetCore.Http;

namespace CafeTrack.Application.Features.Cafes.Commands
{
    public class UpdateCafeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; } // Ensure this is IFormFile
        public string Location { get; set; }
    }
}