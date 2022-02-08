using Common.Utilities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nancy.Session;
using Newtonsoft.Json;
using ShopApplication.Common;
using ShopApplication.Common.Utilities;
using ShopApplication.Common.Utilities.Common.Utilities;
using ShopApplication.DataLayer;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories;

using ShopApplication.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ShopApplication.WebFrameWorks
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               
            }).AddJwtBearer( config =>
            {
                var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
                //var encryptionKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptKey);
                var tokenValidationParameters = new TokenValidationParameters
                {

                    ClockSkew = TimeSpan.Zero,

                    RequireSignedTokens = true,


                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),


                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, // default : false
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuer = true,// default : false
                    ValidIssuer = jwtSettings.Issuer,


                    //TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
                };
                config.TokenValidationParameters = tokenValidationParameters;
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;

                config.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception != null)
                            throw new Exception("Authentication Failed!");
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                      {
                        
                          return Task.CompletedTask;
                      },

                    //This event occurs when we had sent a request that needs authorization and we had not sent token with that request to server.
                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                            throw new Exception( "AuthenticateFailure!");
                        throw new Exception( "You are unauthorized to access this resource!");
                    },

                    
                };

            });

        }

        
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ShopApplicationConnection"));
            });
        }


    }
}
