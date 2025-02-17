using MediatR;

namespace CafeTrack.Application.Features.Employees.Commands
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
