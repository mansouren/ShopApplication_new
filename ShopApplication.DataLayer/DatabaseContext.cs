using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Common.Utilities;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Entities.Common;
using System;


namespace ShopApplication.DataLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        //public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>();
            //new UserConfiguration().Configure(modelBuilder.Entity<User>());
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);


            var DataLayerassembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterEntityTypeConfiguration(DataLayerassembly);
            modelBuilder.RegisterAllEntities<IEntity>(DataLayerassembly);

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                RoleName = "Admin",
                RoleTitle = "مدیر"
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                RoleId = 1,
                UserCode = "666666",
                IsActive = true,
                Mobile = "09123456789",
                Password = SecurityHelper.GetSha256Hash("0020")

            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 2,
                RoleName = "User",
                RoleTitle = "کاربر"
            });

            modelBuilder.Entity<Settings>().HasData(new Settings
            {
                Id = 1,
                FactorIsSend = false,
                PayIsSend = false,
                Name = "فروشگاه اینترنتی"
            });
        }


    }
}
