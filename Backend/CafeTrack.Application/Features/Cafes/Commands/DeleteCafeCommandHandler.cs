using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Application.Features.Cafes.Commands
{
    public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteCafeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _context.Cafes
                .Include(c => c.EmployeeCafes)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (cafe == null)
            {
                return false; // Return false if the cafe doesn't exist
            }

            // Delete all associated employee records
            _context.EmployeeCafes.RemoveRange(cafe.EmployeeCafes);

            // Delete the cafe
            _context.Cafes.Remove(cafe);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

