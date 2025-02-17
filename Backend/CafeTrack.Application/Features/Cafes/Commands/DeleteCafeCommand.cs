using MediatR;

namespace CafeTrack.Application.Features.Cafes.Commands
{
    public class DeleteCafeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}

