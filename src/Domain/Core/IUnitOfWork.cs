using System;
using System.Collections.Generic;
using System.Text;
using NetCoreExampleAuth.Domain.Core.Repositories;

namespace NetCoreExampleAuth.Domain.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        int Complete();
    }
}
