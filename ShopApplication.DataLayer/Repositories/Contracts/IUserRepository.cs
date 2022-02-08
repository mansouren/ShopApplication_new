using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.DataLayer.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<string> AddAsync(User entity, CancellationToken cancellationToken);
        /*Task<User> GetUserByMobileAndPass(string Mobile, string password, CancellationToken cancellationToken)*/
        Task UpdateAsync(User entity, CancellationToken cancellationToken);

    }
}