using ShopApplication.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.DataLayer.Repositories.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddProduct(Product product, CancellationToken cancellationToken);
        Task UpdateProduct(Product product, CancellationToken cancellationToken);
        Task DeleteProduct(Product product, CancellationToken cancellationToken);
       
    }
}
