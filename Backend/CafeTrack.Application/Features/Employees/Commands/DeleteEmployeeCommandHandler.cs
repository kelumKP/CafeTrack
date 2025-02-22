using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Application.Features.Employees.Commands
{
    // DeleteEmployeeCommandHandler
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteEmployeeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (employee == null)
            {
                return false;
            }

            var employeeCafes = await _context.EmployeeCafes
                .Where(ec => ec.EmployeeId == request.Id)
                .ToListAsync(cancellationToken);

            _context.EmployeeCafes.RemoveRange(employeeCafes);
            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

