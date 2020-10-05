using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreExampleAuth.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreExampleAuth.Entities.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        internal static readonly Guid administratorRoleId = Guid.NewGuid();
        internal static readonly Guid managerRoleId = Guid.NewGuid();
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
            new Role
            {
                Id = administratorRoleId,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new Role
            {
                Id = managerRoleId,
                Name = "Manager",
                NormalizedName = "MANAGER"
            }
            );
        }
    }
}
