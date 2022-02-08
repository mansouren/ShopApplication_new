using Microsoft.AspNetCore.Http;
using ShopApplication.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.WebFrameWorks.Scope
{
   public static class SiteLayoutScope
    {
        public static string UserRole { get; set; }
        public static bool IsAuthenticated { get; set; }
    }
}
