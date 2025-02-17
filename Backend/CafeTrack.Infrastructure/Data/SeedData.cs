using CafeTrack.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CafeTrack.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Cafes.Any() || context.Employees.Any())
                {
                    return; // Database has already been seeded
                }

                var cafes = new[]
                {
                    new Cafe { Id = Guid.NewGuid(), Name = "Central Perk", Description = "Cozy coffee shop", Location = "New York" },
                    new Cafe { Id = Guid.NewGuid(), Name = "Cafe Java", Description = "Best espresso in town", Location = "San Francisco" },
                    new Cafe { Id = Guid.NewGuid(), Name = "Blue Bottle Cafe", Description = "Organic and artisanal coffee", Location = "Los Angeles" }
                };

                var employees = new[]
                {
                    new Employee { Id = "UI000001", Name = "Alice Johnson", EmailAddress = "alice@example.com", PhoneNumber = "1234567890", Gender = "Female", StartDate = DateTime.UtcNow },
                    new Employee { Id = "UI000002", Name = "Bob Smith", EmailAddress = "bob@example.com", PhoneNumber = "2345678901", Gender = "Male", StartDate = DateTime.UtcNow },
                    new Employee { Id = "UI000003", Name = "Charlie Brown", EmailAddress = "charlie@example.com", PhoneNumber = "3456789012", Gender = "Male", StartDate = DateTime.UtcNow }
                };

                context.Cafes.AddRange(cafes);
                context.Employees.AddRange(employees);
                context.SaveChanges();

                // Assign employees to cafes
                var employeeCafes = new[]
                {
                    new EmployeeCafe { EmployeeId = employees[0].Id, CafeId = cafes[0].Id },
                    new EmployeeCafe { EmployeeId = employees[1].Id, CafeId = cafes[1].Id },
                    new EmployeeCafe { EmployeeId = employees[2].Id, CafeId = cafes[2].Id }
                };

                context.EmployeeCafes.AddRange(employeeCafes);
                context.SaveChanges();
            }
        }
    }
}

