using ShopApplication.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.Services.Contracts
{
   public interface IProductService
    {
        Task<Product> GetProductById(int? id);
        Task Update(Product product,CancellationToken cancellationToken);
        Task<List<Product>> GetProducts(int id, CancellationToken cancellationToken);
        IQueryable<Product> GetProductsInSearch(string strsearch);
        List<Product> GetAllProducts();
        Task<List<Product>> GetSuggestionProducts(int? id, int? brandid, int? groupid);
    }
}
