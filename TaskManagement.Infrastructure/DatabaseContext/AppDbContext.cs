using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using TaskManagement.Core.Domain.Entities;
using TaskManagement.Core.Domain.Identities;

namespace TaskManagement.Infrastructure.DatabaseContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding Application Roles table
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
            );

            // Seeding Application Users
            var adminUserId = Guid.NewGuid();
            var regularUserId = Guid.NewGuid();
            var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

            var regularUser = new ApplicationUser
            {
                Id = regularUserId,
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            regularUser.PasswordHash = passwordHasher.HashPassword(regularUser, "User@123");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser, regularUser);

            // Assigning Roles to Users
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<Guid>>().HasData(
                new Microsoft.AspNetCore.Identity.IdentityUserRole<Guid> { UserId = adminUserId, RoleId = adminRoleId },
                new Microsoft.AspNetCore.Identity.IdentityUserRole<Guid> { UserId = regularUserId, RoleId = userRoleId }
            );

            // Seeding Task Data
            modelBuilder.Entity<Tasks>().HasData(
                new Tasks
                {
                    TaskId = 1,
                    Title = "Complete work",
                    Description = "Finish the project.",
                    AssignedUserId = adminUserId,
                    DueDate = DateTime.Now.AddDays(5),
                    Status = "InProgress"
                }
            );
        }

        //PendingModelChangesWarning
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
