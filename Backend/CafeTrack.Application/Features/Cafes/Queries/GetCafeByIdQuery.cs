using CafeTrack.Application.DTOs;
using MediatR;

namespace CafeTrack.Application.Features.Cafes.Queries
{
    public class GetCafeByIdQuery : IRequest<CafeDto>
    {
        public Guid Id { get; set; }
    }
}
