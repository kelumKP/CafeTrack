using CafeTrack.Application.DTOs;
using MediatR;

namespace CafeTrack.Application.Features.Cafes.Queries
{
    public class GetCafesByLocationQuery : IRequest<List<CafeDto>>
    {
        public string? Location { get; set; }
    }

}
