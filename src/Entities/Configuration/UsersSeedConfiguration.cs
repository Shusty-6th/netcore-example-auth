using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreExampleAuth.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreExampleAuth.Entities.Configuration
{
    public class UsersSeedConfiguration : IEntityTypeConfiguration<User>
    {
        internal static readonly Guid adminId = Guid.NewGuid();
        internal static readonly Guid managerId = Guid.NewGuid();

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
                LockoutEnabled = true,
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
                LockoutEnabled = true,
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
