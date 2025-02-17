using CafeTrack.Application.DTOs;
using MediatR;

namespace CafeTrack.Application.Features.Employees.Queries
{
    public class GetEmployeesByCafeQuery : IRequest<List<EmployeeDto>>
    {
        public string CafeName { get; set; }
    }
}

