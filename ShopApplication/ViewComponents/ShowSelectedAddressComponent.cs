using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopApplication.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.ViewComponents
{
    public class ShowSelectedAddressComponent : ViewComponent
    {
        private readonly IUserService userService;
        private readonly IShoppingCartService shoppingCartService;

        public ShowSelectedAddressComponent(IUserService userService, IShoppingCartService shoppingCartService)
        {
            this.userService = userService;
            this.shoppingCartService = shoppingCartService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userService.GetUserByMobile(User.Identity.Name);
            var factor =await shoppingCartService.GetFactorByUserId(user.Id);
            var address = await shoppingCartService.GetSelectedAddressForFactor(Convert.ToInt32(factor.AddressId));
            
            return await Task.FromResult((IViewComponentResult)View("ShowAddress", address));
        }
    }
}
