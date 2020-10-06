using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreExampleAuth.Domain.Core.Model;

namespace NetCoreExampleAuth.Domain.Persistence.Configuration
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
