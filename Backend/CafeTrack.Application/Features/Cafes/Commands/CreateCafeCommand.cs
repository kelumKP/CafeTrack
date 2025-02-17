using CafeTrack.Application.DTOs;
using MediatR;

namespace CafeTrack.Application.Features.Cafes.Commands
{
    public class CreateCafeCommand : IRequest<CafeDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; }
        public string Location { get; set; }
        public List<Guid> EmployeeIds { get; set; } // List of employee IDs to associate with the cafe
    }
}
