using Microsoft.AspNetCore.Http;
using ShopApplication.Common;
using ShopApplication.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.Services.Scope
{
   public class UserPanelScope 
    {
        private readonly IUserService userService;
        
        public UserPanelScope(IUserService userService)
        {
            this.userService = userService;
            
        }

        public string GetUserRole(string username)
        {
            return userService.GetUserRole(username);
        }

        
    }
}
