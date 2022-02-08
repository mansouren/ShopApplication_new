using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ShopApplication.DataLayer.Entities;
using ShopApplication.Services;
using ShopApplication.Services.Contracts;
using ShopApplication.WebFrameWorks.Scope;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ShopApplication.WebFrameWorks.Configuration
{
   public static class ApplicationBuilderExtensions
    {
        public static void UseMyMiddleWare(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var userService = context.RequestServices.GetRequiredService<IUserService>();
                var jwtService = context.RequestServices.GetRequiredService<IJwtService>();
                var JWToken = context.Session.GetString("JWToken");
                User user;
                if (!string.IsNullOrEmpty(JWToken))
                {
                    var jwthandler = new JwtSecurityTokenHandler();
                    var jwttoken = jwthandler.ReadToken(JWToken);
                    var expDate = Convert.ToDateTime(jwttoken.ValidTo);
                    
                    if (expDate < DateTime.UtcNow.AddMinutes(1))
                    {
                        user = await userService.GetUserByRefreshToken(context.Request.Cookies["refreshtoken"]);
                        var response = jwtService.GenerateToken(user);
                        await userService.UpdateRefreshToken(response.RefreshToken, user, context.RequestAborted);
                        var cookiOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTime.UtcNow.AddDays(7)
                        };

                        context.Response.Cookies.Append("refreshtoken", response.RefreshToken, cookiOptions);
                        context.Session.SetString("JWToken", response.AccessToken);
                        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                        SiteLayoutScope.UserRole = user.Role.RoleName;
                        SiteLayoutScope.IsAuthenticated = true;
                    }
                    else
                    {
                        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                    }
                
                }
                else
                {
                    bool check = userService.CheckRefreshToken(context.Request.Cookies["refreshtoken"]);
                    if (check)
                    {
                        user = await userService.GetUserByRefreshToken(context.Request.Cookies["refreshtoken"]);
                        var response = jwtService.GenerateToken(user);
                        await userService.UpdateRefreshToken(response.RefreshToken, user, context.RequestAborted);
                        var cookiOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTime.UtcNow.AddDays(7)
                        };

                        context.Response.Cookies.Append("refreshtoken", response.RefreshToken, cookiOptions);
                        context.Session.SetString("JWToken", response.AccessToken);
                        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                        SiteLayoutScope.UserRole = user.Role.RoleName;
                        SiteLayoutScope.IsAuthenticated=true;
                    }
                }

                await next();
            });
        }
    }
}
