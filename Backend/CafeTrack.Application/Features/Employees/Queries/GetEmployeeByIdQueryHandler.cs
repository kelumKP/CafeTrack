using AutoMapper;
using CafeTrack.Application.DTOs;
using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Application.Features.Employees.Queries
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Include(e => e.EmployeeCafes) // Include related data if needed
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (employee == null)
            {
                return null; // Employee not found
            }

            return _mapper.Map<EmployeeDto>(employee); // Map Employee to EmployeeDto
        }
    }
}
