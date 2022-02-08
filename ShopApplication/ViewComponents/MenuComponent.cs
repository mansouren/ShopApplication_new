using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;
using ShopApplication.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.ViewComponents
{
    public class MenuComponent : ViewComponent
    {
        private readonly IRepository<Menu> repository;
        private readonly IUserService userService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IMapper mapper;

        public MenuComponent(IRepository<Menu> repository,IUserService userService,IShoppingCartService shoppingCartService,IMapper mapper)
        {
            this.repository = repository;
            this.userService = userService;
            this.shoppingCartService = shoppingCartService;
            this.mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int myCount = 0;
            if (User.Identity.IsAuthenticated)
            {
                User user = await userService.GetUserByMobile(User.Identity.Name);
                Factor factor =await shoppingCartService.GetFactorByUserId(user.Id);
                if(factor != null)
                {
                    var factorDetails=await shoppingCartService.GetFactorDetailByFactorId(factor.Id);
                    //myCount = factorDetails.Count();
                    myCount = factorDetails.Sum(f => f.ProductCount);
                }
                ViewBag.ShoppingCartCount = myCount;
            }
            var menuDtos = await repository.TableAsNoTracking.ProjectTo<MenuDto>(mapper.ConfigurationProvider)
                          .Where(m => m.NotShow == false).OrderBy(m => m.Order).ToListAsync();
            return await Task.FromResult((IViewComponentResult)View("Menus", menuDtos));
        }                  
    }
}
