using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreExampleAuth.Domain.Core.Model;
using NetCoreExampleAuth.Domain.Core.Repositories;

namespace NetCoreExampleAuth.Domain.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private RepositoryContext Context { get; set; }

        public ProductRepository(RepositoryContext context)
        {
            this.Context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.Context.Products.AsEnumerable();
        }

        public Product GetProductById(int id)
        {
            return this.Context.Products.Find();
        }

        public Product AddProduct(Product product)
        {
            return this.Context.Products.Add(product).Entity;
        }
    }
}
