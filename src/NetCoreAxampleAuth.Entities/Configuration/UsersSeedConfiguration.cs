using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreAxampleAuth.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreAxampleAuth.Entities.Configuration
{
    public class UsersSeedConfiguration : IEntityTypeConfiguration<User>
    {
        internal const string adminId = "fb859dc0-1e55-4cae-821b-9e3e863757b4";
        internal const string managerId = "5441637f-6290-4925-8afa-dab9254ea8a8";

        public void Configure(EntityTypeBuilder<User> builder)
        {
            var admin = new User
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "Admin@Admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                PhoneNumber = "XXXXXXXXXXXXX",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString("D"),
            };

            admin.PasswordHash = PassGenerate(admin, "Admin1234!");

            var manager = new User
            {
                Id = managerId,
                UserName = "manager",
                NormalizedUserName = "MANAGER",
                FirstName = "Manager",
                LastName = "Manager",
                Email = "Manager@Manager.com",
                NormalizedEmail = "MANAGER@MANAGER.COM",
                PhoneNumber = "XXXXXXXXXXXXX",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString("D"),
            };

            manager.PasswordHash = PassGenerate(manager, "Manager1234!");

            builder.HasData(admin, manager);
        }

        public string PassGenerate(User user, string password)
        {
            var passHash = new PasswordHasher<User>();
            return passHash.HashPassword(user, password);
        }
    }
}
