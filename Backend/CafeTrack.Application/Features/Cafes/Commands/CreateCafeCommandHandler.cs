using CafeTrack.Application.DTOs;
using CafeTrack.Core.Entities;
using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace CafeTrack.Application.Features.Cafes.Commands
{
    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, CafeDto>
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateCafeCommandHandler(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<CafeDto> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            // Ensure the WebRootPath is not null
            if (string.IsNullOrEmpty(_env.WebRootPath))
            {
                throw new InvalidOperationException("WebRootPath is not configured.");
            }

            // Save the logo file to the file system
            string logoUrl = null;
            if (request.Logo != null)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

                // Ensure the uploads directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Logo.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Logo.CopyToAsync(fileStream, cancellationToken);
                }

                logoUrl = $"/uploads/{uniqueFileName}";
            }

            // Create the cafe entity
            var cafe = new Cafe
            {
                Name = request.Name,
                Description = request.Description,
                Logo = logoUrl, // Save the file URL
                Location = request.Location
            };

            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync(cancellationToken);

            // Return the cafe DTO
            return new CafeDto
            {
                Id = cafe.Id,
                Name = cafe.Name,
                Description = cafe.Description,
                Logo = cafe.Logo,
                Location = cafe.Location
            };
        }
    }
}
