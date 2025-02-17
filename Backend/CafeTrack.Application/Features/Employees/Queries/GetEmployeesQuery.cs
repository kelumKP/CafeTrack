using CafeTrack.Application.DTOs;
using MediatR;

namespace CafeTrack.Application.Features.Employees.Queries
{
    public class GetEmployeesQuery : IRequest<List<EmployeeDto>> { }
}
