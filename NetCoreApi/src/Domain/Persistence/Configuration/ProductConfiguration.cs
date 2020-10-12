using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreExampleAuth.Domain.Core.Model;

namespace NetCoreExampleAuth.Domain.Persistence.Configuration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (
                new Product()
                {
                    Id = 1,
                    Name = "Bike",
                    Color = ProductColor.Blue,
                    IsGoodQuality = true
                },
                new Product()
                {
                    Id = 2,
                    Name = "Jogging pants",
                    Color = ProductColor.Red
                },
                new Product()
                {
                    Id = 3,
                    Name = "Ball",
                    Color = ProductColor.Green,
                    IsGoodQuality = true
                },
                new Product()
                {
                    Id = 4,
                    Name = "T-shirt",
                    Color = ProductColor.Blue,
                    IsGoodQuality = true
                },
                new Product()
                {
                    Id = 5,
                    Name = "Protein Juice",
                    Color = ProductColor.None,
                    IsGoodQuality = true
                }
            );
        }
    }
}