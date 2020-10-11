using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCoreExampleAuth.Domain.Persistence.Configuration
{
    class UsersInRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            IdentityUserRole<Guid> adminInRole = new IdentityUserRole<Guid>
            {
                RoleId = RoleConfiguration.administratorRoleId,
                UserId = UsersSeedConfiguration.adminId
            };

            IdentityUserRole<Guid> managerInRole = new IdentityUserRole<Guid>
            {
                RoleId = RoleConfiguration.managerRoleId,
                UserId = UsersSeedConfiguration.managerId
            };

            builder.HasData(adminInRole, managerInRole);
        }
    }
}
