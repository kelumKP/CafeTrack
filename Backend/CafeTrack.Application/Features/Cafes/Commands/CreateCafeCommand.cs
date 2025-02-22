using CafeTrack.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CafeTrack.Application.Features.Cafes.Commands
{
    public class CreateCafeCommand : IRequest<CafeDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public IFormFile Logo { get; set; } 
    }
}
