using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NetCoreExampleAuth.Domain.Core.Model;

namespace NetCoreExampleAuth.Domain.Core.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);

        Product AddProduct(Product product);

        void RemoveProduct(Product product);
    }
}
