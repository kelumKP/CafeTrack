using CafeTrack.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Cafe> Cafes { get; set; }
        public virtual DbSet<EmployeeCafe> EmployeeCafes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure unique index for Employee.EmailAddress
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmailAddress)
                .IsUnique();

            // Configure the many-to-many relationship between Employee and Cafe
            modelBuilder.Entity<EmployeeCafe>()
                .HasKey(ec => new { ec.EmployeeId, ec.CafeId }); // Composite primary key

            modelBuilder.Entity<EmployeeCafe>()
                .HasOne(ec => ec.Employee)
                .WithMany(e => e.EmployeeCafes)
                .HasForeignKey(ec => ec.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if Employee is deleted

            modelBuilder.Entity<EmployeeCafe>()
                .HasOne(ec => ec.Cafe)
                .WithMany(c => c.EmployeeCafes)
                .HasForeignKey(ec => ec.CafeId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if Cafe is deleted

            // Ensure an employee can only work at one café (unique index)
            modelBuilder.Entity<EmployeeCafe>()
                .HasIndex(ec => ec.EmployeeId)
                .IsUnique();
        }
    }
}
