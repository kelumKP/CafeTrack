using MediatR;
using CafeTrack.Application.DTOs;

namespace CafeTrack.Application.Features.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest<EmployeeDto>
    {
        public EmployeeDto Employee { get; set; } // Employee data without CafeId
        
    }
}
