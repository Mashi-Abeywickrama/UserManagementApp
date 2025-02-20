using Microsoft.EntityFrameworkCore;
using UserManagementApp.Models;

namespace UserManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserDetails> Users { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleType>().HasData(
                new RoleType { RoleID = 1, RoleName = "Admin" },
                new RoleType { RoleID = 2, RoleName = "User" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusID = 1, StatusName = "Active" },
                new Status { StatusID = 2, StatusName = "Inactive" }
            );
        }
    }
}
