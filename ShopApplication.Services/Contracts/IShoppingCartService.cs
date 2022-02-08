using ShopApplication.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.Services.Contracts
{
   public interface IShoppingCartService
    {
        Task<Address> GetSelectedAddressForFactor(int addressid);
        Task<IEnumerable<Address>> GetAddressByUser(int userid);
        Task UpdateProduct(Product product, CancellationToken cancellationToken);
        Task<Factor> GetFactorByFactorNumber(string ordernumber);
        Task UpdateFactor(Factor factor, CancellationToken cancellationToken);
        Task UpdateFactorDetail(FactorDetail factorDetail, CancellationToken cancellationToken);
        Task<User> GetUserbyMobile(string mobile);
        Task DeleteFactorDetail(int factorDetailid,CancellationToken cancellationToken);
        Task<Factor> GetFactorByUserId(int userid);
        Task<IEnumerable<FactorDetail>> GetFactorDetailByFactorId(int factorid);
        Task<Product> GetProductById(int id); 
        Task AddToCart(int? productid, int productCount, string mobile, CancellationToken cancellationToken);
    }
}
