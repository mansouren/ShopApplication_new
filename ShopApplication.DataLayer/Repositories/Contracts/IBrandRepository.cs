using ShopApplication.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.DataLayer.Repositories.Contracts
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task AddBrand(Brand brand, CancellationToken cancellationToken);
        Task UpdateBrand(Brand brand, CancellationToken cancellationToken);
        Task DeleteBrand(Brand brand, CancellationToken cancellationToken);
    }
}
