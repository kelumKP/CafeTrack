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
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmailAddress)
                .IsUnique();

            modelBuilder.Entity<EmployeeCafe>()
                .HasKey(ec => new { ec.EmployeeId, ec.CafeId });  // Composite key for EmployeeCafe

            modelBuilder.Entity<EmployeeCafe>()
                .HasOne(ec => ec.Employee)
                .WithMany(e => e.EmployeeCafes)
                .HasForeignKey(ec => ec.EmployeeId);

            modelBuilder.Entity<EmployeeCafe>()
                .HasOne(ec => ec.Cafe)
                .WithMany(c => c.EmployeeCafes)
                .HasForeignKey(ec => ec.CafeId);

            modelBuilder.Entity<EmployeeCafe>()
                .HasIndex(ec => new { ec.EmployeeId, ec.CafeId })
                .IsUnique();  // Ensure employee works only at one café
        }
    }
}
