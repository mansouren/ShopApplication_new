
using ShopApplication.Common;
using ShopApplication.Common.Utilities;
using ShopApplication.DataLayer.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace ShopApplication.DataLayer.Repositories
{
    public class UserRepository : Repository<User>, IScopeDependency, IUserRepository
    {

        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }


        public async Task<string> AddAsync(User entity, CancellationToken cancellationToken)
        {

            Random random = new Random();
            int myCode = random.Next(100000, 900000); //Produce 6 digit random numbers
            User user = new User
            {
                UserCode = myCode.ToString(),
                Mobile = entity.Mobile,
                Password = SecurityHelper.GetSha256Hash(entity.Password),
                RoleId = 2

            };

            await base.AddAsync(user, cancellationToken);

            string text = "ثبت نام شما در فروشگاه انجام شد" + Environment.NewLine + "کد فعالسازی شما : " + user.UserCode;
            return text;

        }

        
        public async Task UpdateAsync(User entity, CancellationToken cancellationToken)
        {
            //entity.Password = entity.Password;
            Random random = new Random();
            string myCode = random.Next(100000, 900000).ToString();
            entity.UserCode = myCode;
            await base.UpdateAsync(entity, cancellationToken);
        }
    }
}
