﻿using CafeTrack.Application.DTOs;
using MediatR;

namespace CafeTrack.Application.Features.Employees.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public string Id { get; set; }
    }
}
