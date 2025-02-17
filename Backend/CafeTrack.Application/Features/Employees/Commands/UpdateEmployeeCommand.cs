using MediatR;
using CafeTrack.Application.DTOs;

namespace CafeTrack.Application.Features.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest<EmployeeDto>
    {
        public string Id { get; set; }
        public EmployeeDto Employee { get; set; } // Employee data without CafeId
        public string CafeId { get; set; }  // CafeId can be a separate property
    }
}
