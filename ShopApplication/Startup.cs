using Autofac;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc.Authorization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopApplication.Common;
using Microsoft.Extensions.Caching.Memory;
using ShopApplication.WebFrameWorks;
using ShopApplication.WebFrameWorks.Configuration;
using ShopApplication.WebFrameWorks.CustomMapping;
using System.Text;
using ShopApplication.DataLayer;
using System.Collections;
using ShopApplication.Services.Scope;

namespace ShopApplication
{
    public class Startup
    {
        private readonly SiteSettings _siteSettings;
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var key = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.SecretKey);
            var encKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.EncryptKey);

            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            
            services.AddMvc(options => options.Filters.Add(new AuthorizeFilter()))
                    .AddFluentValidation(fv => {
                        fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                        fv.RegisterValidatorsFromAssemblyContaining<DatabaseContext>();
                        });
            
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddSession();
            services.AddHttpClient();
            services.AddDatabaseContext(Configuration);
            services.InitializeAutoMapper();
            services.AddJwtAuthentication(_siteSettings.JwtSettings);
            services.AddScoped<UserPanelScope>();
            services.AddScoped<ShowProductScope>();
            services.AddCloudscribePagination();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMemoryCache Cache)
        {
            if (env.EnvironmentName.Equals("Development"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMyMiddleWare(); //Customize Middleware

            //Who are you?
            app.UseAuthentication();

            //Are You Allowed?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "areas",
                //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                //);

                endpoints.MapRazorPages();
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddServices();
        }
    }
}
