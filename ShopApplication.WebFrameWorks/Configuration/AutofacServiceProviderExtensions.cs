using Autofac;
using ShopApplication.Common;
using ShopApplication.DataLayer;
using ShopApplication.DataLayer.Repositories;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.WebFrameWorks.Configuration
{
   public static class AutofacServiceProviderExtensions
    {
        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            
            var dataLayerAssembly = typeof(DatabaseContext).Assembly;
            var ServiceAssembly = typeof(UserService).Assembly;

            containerBuilder.RegisterAssemblyTypes(dataLayerAssembly,ServiceAssembly)
                .AssignableTo<IScopeDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(dataLayerAssembly, ServiceAssembly)
                .AssignableTo<ISingletoneDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder.RegisterAssemblyTypes(dataLayerAssembly, ServiceAssembly)
                .AssignableTo<ITransiantDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

           
        }
    }
}
