using System;
using System.Collections.Generic;
using System.Text;
using NetCoreExampleAuth.Domain.Core;
using NetCoreExampleAuth.Domain.Core.Repositories;

namespace NetCoreExampleAuth.Domain.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryContext context;

        public UnitOfWork(
            RepositoryContext context,
            IProductRepository products
            )
        {
            this.context = context;
            this.Products = products;
        }

        public IProductRepository Products { get; }

        /// <summary>
        /// Completes this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Complete()
        {
            return context.SaveChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
