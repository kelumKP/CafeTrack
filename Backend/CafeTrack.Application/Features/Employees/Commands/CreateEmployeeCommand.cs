using CafeTrack.Application.DTOs;
using MediatR;

namespace CafeTrack.Application.Features.Employees.Commands
{
    public class CreateEmployeeCommand : IRequest<EmployeeDto> // Make sure to inherit IRequest<EmployeeDto>
    {
        public EmployeeDto Employee { get; set; }
    }
}
