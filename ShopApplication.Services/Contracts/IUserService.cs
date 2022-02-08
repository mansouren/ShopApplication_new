using ShopApplication.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.Services.Contracts
{
   public interface IUserService
    {
        Task<bool> IsExistMobile(string mobile);
        Task<User> IsExistUser(string mobile, string password);
        Task<User> GetUserByUserCodeAndMobile(string userCode, string mobile);
        Task<User> GetUserByMobile(string mobile);
        //Task<User> GetUserByMobileAndPawword(string mobile, string password);
        Task<User> GetUserByUserCode(string usercode);
        Task ForgetPassword(string password, User user, CancellationToken cancellationToken);
        Task ChangePassword(string password,User user, CancellationToken cancellationToken);
        Task UpdateRefreshToken(string rftoken, User user, CancellationToken cancellationToken);
        bool CheckRefreshToken(string rftoken);
        Task<User> GetUserByRefreshToken(string rftoken);
        string GetUserRole(string username);
        Task AddUser(User user, CancellationToken cancellationToken);
        Task UpdateUser(User user, CancellationToken cancellationToken);
        Task<User> GetUserById(int id, CancellationToken cancellationToken);
    }
}
