using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreAxampleAuth.Entities.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreAxampleAuth.Entities.Configuration
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
                }
            );
        }
    }
}