using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreAxampleAuth.Entities.Configuration
{
    class UsersInRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            IdentityUserRole<string> adminInRole = new IdentityUserRole<string>
            {
                RoleId = RoleConfiguration.administratorRoleId,
                UserId = UsersSeedConfiguration.adminId
            };

            IdentityUserRole<string> managerInRole = new IdentityUserRole<string>
            {
                RoleId = RoleConfiguration.managerRoleId,
                UserId = UsersSeedConfiguration.managerId
            };

            builder.HasData(adminInRole, managerInRole);
        }
    }
}
