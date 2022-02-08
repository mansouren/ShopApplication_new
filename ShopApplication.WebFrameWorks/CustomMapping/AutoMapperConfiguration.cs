using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using System.Linq;

namespace ShopApplication.WebFrameWorks.CustomMapping
{
   public static class AutoMapperConfiguration
    {
        public static void InitializeAutoMapper(this IServiceCollection services,params Assembly[] assemblies)
        {
            services.AddAutoMapper(config =>
            {
                config.AddCustomMappingProfile();
                config.Advanced.BeforeSeal(congiProvider => congiProvider.CompileMappings());
            },assemblies);
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression configurationExpression)
        {
            configurationExpression.AddCustomMappingProfile(Assembly.GetEntryAssembly());
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression configurationExpression,params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(type => type.GetExportedTypes());
            var list = allTypes.Where(type => type.IsClass && !type.IsAbstract
            && type.GetInterfaces().Contains(typeof(IHaveCustomMapping)))
                .Select(type => (IHaveCustomMapping)Activator.CreateInstance(type));

            var profile = new CustomMappingProfile(list);
            configurationExpression.AddProfile(profile);
        }
    }
}
