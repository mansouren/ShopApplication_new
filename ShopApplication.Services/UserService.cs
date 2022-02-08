using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Common;
using ShopApplication.Common.Utilities;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories;
using ShopApplication.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ShopApplication.Services
{
    public class UserService : IUserService, IScopeDependency
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<bool> IsExistMobile(string mobile)
        {
            var exist = await userRepository.TableAsNoTracking.AnyAsync(u => u.Mobile == mobile);
            return exist;
        }

        public Task<User> IsExistUser(string mobile, string password)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            var model = userRepository.Table.Where(u => u.Mobile == mobile && u.Password == passwordHash)
                       .Include(u=>u.Role).SingleOrDefaultAsync();

            return model;
        }

        public async Task<User> GetUserByUserCodeAndMobile(string userCode, string mobile)
        {
            var user = await userRepository.Table
                .FirstOrDefaultAsync(u => u.UserCode == userCode && u.Mobile == mobile);
            return user;
        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            var user = await userRepository.TableAsNoTracking.FirstOrDefaultAsync(u => u.Mobile == mobile);
            return user;
        }

        public async Task<User> GetUserByUserCode(string usercode)
        {
            var user = await userRepository.TableAsNoTracking.FirstOrDefaultAsync(u => u.UserCode == usercode);
            return user;
        }

        public async Task ForgetPassword(string password, User user, CancellationToken cancellationToken)
        {
            Random random = new Random();
            var myCode = random.Next(100000, 900000);
            user.Password = SecurityHelper.GetSha256Hash(password);
            user.UserCode = myCode.ToString();
            await userRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task UpdateRefreshToken(string rftoken, User user, CancellationToken cancellationToken)
        {
            user.RefreshToken = rftoken;
            await userRepository.UpdateAsync(user, cancellationToken);
        }

        public  bool CheckRefreshToken(string rftoken)
        {
            bool status =  userRepository.Table.Any(u => u.RefreshToken == rftoken);
            return status;
        }

        public async Task<User> GetUserByRefreshToken(string rftoken)
        {
            var user =await userRepository.Table.Include(u => u.Role).SingleOrDefaultAsync(u => u.RefreshToken == rftoken);
            return user;
        }

        public string GetUserRole(string username)
        {
           var userRole = userRepository.TableAsNoTracking.Include(u => u.Role).SingleOrDefault(u => u.Mobile == username).Role.RoleName;
           return userRole;
        }

        public async Task ChangePassword(string password, User user, CancellationToken cancellationToken)
        {
            string hashPassword = SecurityHelper.GetSha256Hash(password);
            user.Password = hashPassword;
            await userRepository.UpdateAsync(user, cancellationToken);
        }

        //public async Task<User> GetUserByMobileAndPawword(string mobile, string password)
        //{
        //    string hashPass = SecurityHelper.GetSha256Hash(password);
        //    var user =await userRepository.TableAsNoTracking.Include(x => x.Role).FirstOrDefaultAsync(x => x.Mobile == mobile && x.Password == hashPass);
        //    return user;
        //}

        public async Task AddUser(User user, CancellationToken cancellationToken)
        {
            user.Password = SecurityHelper.GetSha256Hash(user.Password);
            Random random = new Random();
            var mycode = random.Next(100000, 900000);
            user.UserCode = mycode.ToString();
            await userRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task UpdateUser(User user, CancellationToken cancellationToken)
        {
            user.Password = SecurityHelper.GetSha256Hash(user.Password);
            await userRepository.UpdateAsync(user, cancellationToken);
        }

        public Task<User> GetUserById(int id, CancellationToken cancellationToken)
        {
           return userRepository.Table.Include(u => u.Role).SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
