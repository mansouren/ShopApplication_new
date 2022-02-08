using Microsoft.EntityFrameworkCore;
using ShopApplication.Common;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.Services
{
    public class ProductService : IProductService,IScopeDependency
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<Product> GetAllProducts()
        {
            return productRepository.TableAsNoTracking.ToList();
        }

        public async Task<Product> GetProductById(int? id)
        {
            return await productRepository.Table.Include(p => p.Brand).Include(p => p.Group)
                   .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProducts(int id, CancellationToken cancellationToken)
        {
            return await productRepository.TableAsNoTracking.Where(p => p.Id == id).ToListAsync();
        }

        public IQueryable<Product> GetProductsInSearch(string strsearch)
        {
            var products = productRepository.TableAsNoTracking.Include(p => p.Brand).Include(p => p.Group)
                           .Where(p => p.NotShow == false && p.Name.Contains(strsearch) || p.Description.Contains(strsearch)
                            || p.Brand.Title.Contains(strsearch) || p.Group.Name.Contains(strsearch));
            return products;
        }

        public async Task<List<Product>> GetSuggestionProducts(int? id, int? brandid, int? groupid)
        {
           var products =await productRepository.TableAsNoTracking
                .Where(p => p.NotShow == false && p.Id != id
                       && (p.BrandId == brandid || p.GroupId == groupid))
                .OrderBy(p => Guid.NewGuid()).ToListAsync();
            return products;
        }

        public async Task Update(Product product, CancellationToken cancellationToken)
        {
           await productRepository.UpdateAsync(product, cancellationToken);
        }
    }
}
