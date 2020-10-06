using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreExampleAuth.Domain.Core.Model;
using NetCoreExampleAuth.Domain.Persistence.Configuration;

namespace NetCoreExampleAuth.Domain.Persistence
{
    public class RepositoryContext : IdentityDbContext<User, Role, Guid>
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            // WARN: disable it for security reason. These users with default passwords are for demo purposes only!
            modelBuilder.ApplyConfiguration(new UsersSeedConfiguration());
            modelBuilder.ApplyConfiguration(new UsersInRolesConfiguration());
        }
        public DbSet<Product> Products { get; set; }
    }
}
