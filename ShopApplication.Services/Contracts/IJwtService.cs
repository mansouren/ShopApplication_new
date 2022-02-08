using ShopApplication.Common;
using ShopApplication.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.Services
{
    public interface IJwtService
    {
        Response GenerateToken(User user);
    
    }
}