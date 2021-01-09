using Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Models
{
    public class CompanyDatabaseContext : IdentityDbContext
    {
        public CompanyDatabaseContext(DbContextOptions<CompanyDatabaseContext> options) : base(options)
        {


        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRoles>(e =>
            {
                e.HasKey(ur => new { ur.UserId, ur.RoleId });
                e.HasOne(ur => ur.ApplicationRole)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                e.HasOne(ur => ur.ApplicationUser)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            new IdentityRoleClaim<Guid>()
            {
                Id = 1,
                RoleId = Guid.Parse("0a2f50ca-f27d-48a3-b6ba-58fadf4d1c44"),
                ClaimType = "testing1",
                ClaimValue = "testing2"
            };

            modelBuilder.Entity<ApplicationUser>(e => { e.ToTable(name: "User", schema: "TJ_PLT"); });
            modelBuilder.Entity<ApplicationRole>(e => { e.ToTable(name: "Role", schema: "TJ_PLT"); });
            modelBuilder.Entity<ApplicationUserRoles>(e => { e.ToTable(name: "UserRoles", schema: "TJ_PLT"); });
            modelBuilder.Entity<IdentityUserClaim<Guid>>(e => { e.ToTable(name: "UserClaim", schema: "TJ_PLT"); });
            modelBuilder.Entity<IdentityUserLogin<Guid>>(e => { e.ToTable(name: "UserLogin", schema: "TJ_PLT"); });
            modelBuilder.Entity<IdentityUserToken<Guid>>(e => { e.ToTable(name: "UserToken", schema: "TJ_PLT"); });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(e => { e.ToTable(name: "RoleClaim", schema: "TJ_PLT"); });

            modelBuilder.Entity<ApplicationUser>().HasData(
                    new ApplicationUser()
                    {
                        Id = Guid.Parse("4b02c6bd-5aed-4fa2-867b-a6abdd5b7d5e"),
                        Email = "backoffice@jrny.co.uk",
                        NormalizedEmail = "BACKOFFICE1@JRNY.CO.UK",
                        UserName = "backoffice@jrny.co.uk",
                        NormalizedUserName = "BACKOFFICE@JRNY.CO.UK",
                        PasswordHash = "AQAAAAEAACcQAAAAEMAtPzpJ8lU39+9yfGq1UGB/ANyDCkgGPCk7OlMKgcTJpQnAaqGdLSDp2BFilEa3nA==",
                        SecurityStamp = "5CYIPYXP45F2FXJ3JJLQJLGWPIMB7XI3",
                        ConcurrencyStamp = "6a979ee8-2a46-4d18-86fe-d960c6437705",
                        LockoutEnabled = true,
                        EmailConfirmed = true
                    }

                );



            modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationRole()
            {
                Id = Guid.Parse("8388c9f2-8f14-4a01-95bd-2ebd40542a63"),
                Name = "Administrator",
                NormalizedName = "ADMIN"
            });
            modelBuilder.Entity<ApplicationUserRoles>()
                .HasData(
                    new ApplicationUserRoles()
                    {
                        UserId = Guid.Parse("4b02c6bd-5aed-4fa2-867b-a6abdd5b7d5e"),
                        RoleId = Guid.Parse("ed1072df-4049-46a7-be4a-03f12d6fe396")
                    });


        }
    }
}
