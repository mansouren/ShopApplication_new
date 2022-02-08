using Common.Utilities;
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
    public class ShoppingCartService : IShoppingCartService, IScopeDependency
    {
        private readonly IUserService userService;
        private readonly IRepository<Factor> factorRepo;
        private readonly IProductService productService;
        private readonly IRepository<FactorDetail> factorDetailRepo;
        private readonly IRepository<Address> addressRepository;

        public ShoppingCartService(IUserService userService
                                   , IRepository<Factor> factorRepo
                                   , IProductService productService
                                   , IRepository<FactorDetail> factorDetailRepo
                                   ,IRepository<Address> addressRepository)
        {
            this.userService = userService;
            this.factorRepo = factorRepo;
            this.productService = productService;
            this.factorDetailRepo = factorDetailRepo;
            this.addressRepository = addressRepository;
        }
        public async Task AddToCart(int? productid, int productCount, string mobile,CancellationToken cancellationToken)
        {
            User user = await userService.GetUserByMobile(mobile);
            Factor factor = await factorRepo.TableAsNoTracking.FirstOrDefaultAsync(f => f.UserId == user.Id && f.IsPayed == false);
            Product product = await productService.GetProductById(productid);
            if (factor != null)
            {
                FactorDetail factorDetail= await factorDetailRepo.TableAsNoTracking.FirstOrDefaultAsync(d => d.ProductId == product.Id && d.FactorId == factor.Id);
                if (factorDetail != null)
                {
                    factorDetail.ProductCount += productCount;
                    factorDetail.UnitPrice = product.Price;
                    await factorDetailRepo.UpdateAsync(factorDetail, cancellationToken);
                }
                else
                {
                    FactorDetail newfactorDetail = new FactorDetail
                    {
                        FactorId = factor.Id,
                        ProductId = Convert.ToInt32(productid),
                        ProductCount = productCount,
                        UnitPrice = product.Price
                    };

                    await factorDetailRepo.AddAsync(newfactorDetail, cancellationToken);
                }
            }
            else
            {
                var address =await addressRepository.TableAsNoTracking.FirstOrDefaultAsync(a => a.UserId == user.Id);
                int addressId = 0;
                if(address != null)
                {
                    addressId = address.Id;
                }
                Factor newFactor = new Factor
                {
                    UserId = user.Id,
                    Number = StringExtensions.GetRandomNumber(),
                    CreateDate = DateTime.Now,
                    IsPayed = false,
                    Price = 0,
                    AddressId = addressId
                };
                await factorRepo.AddAsync(newFactor, cancellationToken);
                FactorDetail factorDetail = new FactorDetail
                {
                    FactorId = newFactor.Id,
                    ProductId = product.Id,
                    UnitPrice = product.Price,
                    ProductCount = productCount
                };
                await factorDetailRepo.AddAsync(factorDetail, cancellationToken);

            }
        }

        public async Task DeleteFactorDetail(int factorDetailid, CancellationToken cancellationToken)
        {
            var factorDetail =await factorDetailRepo.GetByIdAsync(cancellationToken, factorDetailid);
            await factorDetailRepo.DeleteAsync(factorDetail, cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressByUser(int userid)
        {
           return await addressRepository.TableAsNoTracking.Where(a => a.UserId == userid).ToListAsync();
        }

        public async Task<Factor> GetFactorByFactorNumber(string ordernumber)
        {
            var factor =await factorRepo.TableAsNoTracking.FirstOrDefaultAsync(f => f.Number == ordernumber);
            return factor;
        }

        public async Task<Factor> GetFactorByUserId(int userid)
        {
           var factor = await factorRepo.TableAsNoTracking.FirstOrDefaultAsync(f => f.IsPayed == false && f.UserId == userid);
            return factor;
        }

        public async Task<IEnumerable<FactorDetail>> GetFactorDetailByFactorId(int factorid)
        {
            var factordetail =await factorDetailRepo.TableAsNoTracking
                .Include(d => d.Product)
                .Include(d => d.Factor)
                .Where(d => d.FactorId == factorid).ToListAsync();
            return factordetail;
        }

        public async Task<Product> GetProductById(int id)
        {
           return await productService.GetProductById(id);

        }

        public async Task<Address> GetSelectedAddressForFactor(int addressid)
        {
           return await addressRepository.TableAsNoTracking.Include(a => a.City).ThenInclude(c => c.State)
                .FirstOrDefaultAsync(a => a.Id == addressid);
        }

        public async Task<User> GetUserbyMobile(string mobile)
        {
            return await userService.GetUserByMobile(mobile);
        }

        public async Task UpdateFactor(Factor factor, CancellationToken cancellationToken)
        {
            await factorRepo.UpdateAsync(factor, cancellationToken);
        }

        public async Task UpdateFactorDetail(FactorDetail factorDetail, CancellationToken cancellationToken)
        {
            await factorDetailRepo.UpdateAsync(factorDetail, cancellationToken);
        }

        public async Task UpdateProduct(Product product, CancellationToken cancellationToken)
        {
            await productService.Update(product, cancellationToken);
        }
    }
}
