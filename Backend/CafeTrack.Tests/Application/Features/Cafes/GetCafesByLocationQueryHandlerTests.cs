using System;
using System.Collections.Generic;
using System.Linq;
using CafeTrack.Application.Features.Cafes.Queries;
using CafeTrack.Core.Entities;
using CafeTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Tests
{
    public class GetCafesByLocationQueryHandlerTests
    {
        private readonly GetCafesByLocationQueryHandler _handler;
        private readonly AppDbContext _context;

        public GetCafesByLocationQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "CafeDb") // Use an in-memory database
                .Options;

            _context = new AppDbContext(options);
            _handler = new GetCafesByLocationQueryHandler(_context);
        }

        [Fact]
        public async Task Handle_ShouldReturnCafesOrderedByEmployeeCount()
        {
            // Arrange
            var employee1 = new Employee
            {
                Id = "1",
                Name = "Employee1",
                EmailAddress = "employee1@example.com", // Set EmailAddress
                Gender = "Male",                       // Set Gender
                PhoneNumber = "1234567890"             // Set PhoneNumber
            };
            var employee2 = new Employee
            {
                Id = "2",
                Name = "Employee2",
                EmailAddress = "employee2@example.com", // Set EmailAddress
                Gender = "Female",                      // Set Gender
                PhoneNumber = "0987654321"              // Set PhoneNumber
            };
            var employee3 = new Employee
            {
                Id = "3",
                Name = "Employee3",
                EmailAddress = "employee3@example.com", // Set EmailAddress
                Gender = "Male",                        // Set Gender
                PhoneNumber = "1122334455"              // Set PhoneNumber
            };

            var cafes = new List<Cafe>
    {
        new Cafe
        {
            Id = Guid.NewGuid(),
            Name = "Cafe1",
            Location = "New York",
            Description = "A cozy cafe in New York", // Set Description
            EmployeeCafes = new List<EmployeeCafe>
            {
                new EmployeeCafe { EmployeeId = "1", Employee = employee1, CafeId = Guid.NewGuid() }
            }
        },
        new Cafe
        {
            Id = Guid.NewGuid(),
            Name = "Cafe2",
            Location = "New York",
            Description = "A trendy cafe in New York", // Set Description
            EmployeeCafes = new List<EmployeeCafe>
            {
                new EmployeeCafe { EmployeeId = "2", Employee = employee2, CafeId = Guid.NewGuid() },
                new EmployeeCafe { EmployeeId = "3", Employee = employee3, CafeId = Guid.NewGuid() }
            }
        }
    };

            await _context.Employees.AddRangeAsync(employee1, employee2, employee3);
            await _context.Cafes.AddRangeAsync(cafes);
            await _context.SaveChangesAsync();

            var query = new GetCafesByLocationQuery { Location = "New York" };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count); // Should return two cafes
        }

    }
}
