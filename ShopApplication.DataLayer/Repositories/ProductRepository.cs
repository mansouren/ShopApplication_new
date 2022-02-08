using Common.Utilities;
using ShopApplication.Common;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.DataLayer.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository, IScopeDependency
    {
        public ProductRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task AddProduct(Product product, CancellationToken cancellationToken)
        {
            if(product.ImgFile != null)
            {
                string extension = Path.GetExtension(product.ImgFile.FileName);
                string filename = StringExtensions.GetFileName();
                product.Img = filename + extension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/", product.Img);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                   await product.ImgFile.CopyToAsync(stream);
                }
            }
            product.Seen = 0;
            await base.AddAsync(product, cancellationToken);
        }

        public async Task DeleteProduct(Product product, CancellationToken cancellationToken)
        {
            if(product.Img != null)
            {
                string deletePath = Path.Combine("wwwroot/Images/Product/", product.Img);
                if (File.Exists(deletePath))
                {
                    File.Delete(deletePath);
                }
            }
            await base.DeleteAsync(product, cancellationToken);
        }

        public async Task UpdateProduct(Product product, CancellationToken cancellationToken)
        {
            if(product.ImgFile != null)
            {
                if(product.Img != string.Empty)
                {
                    string deletePath = Path.Combine("wwwroot/Images/Product/", product.Img);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                string extension = Path.GetExtension(product.ImgFile.FileName);
                string filename = StringExtensions.GetFileName();
                product.Img = filename + extension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/", product.Img);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await product.ImgFile.CopyToAsync(stream);
                }
            }
            
            await base.UpdateAsync(product, cancellationToken);
        }
    }
}
