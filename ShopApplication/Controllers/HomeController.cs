using AutoMapper;
using AutoMapper.QueryableExtensions;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopApplication.Common.Utilities;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;
using ShopApplication.Services.Contracts;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IRepository<Menu> repository;
        private readonly IProductService productService;
       
        private readonly IMapper mapper;
        private readonly IShoppingCartService shoppingCartService;

        public HomeController(IRepository<Menu> repository
            ,IProductService productService, IMapper mapper, IShoppingCartService shoppingCartService)
        {
            this.repository = repository;
            this.productService = productService;
            
            this.mapper = mapper;
            this.shoppingCartService = shoppingCartService;
        }
        public IActionResult Index()
        {
            return View();
        }

      
        public IActionResult Filter(string strsearch, int? pageNumber = 1,int? pageSize=2)
        {
            var query = productService.GetProductsInSearch(strsearch);
            int excludeRecords =Convert.ToInt32((pageSize * pageNumber) - pageSize);
            var products = query.Skip(excludeRecords).Take((int)pageSize);
            var result = new PagedResult<Product>
            {
                Data = products.ToList(),
                TotalItems = query.ToList().Count(),
                PageNumber =(int) pageNumber,
                PageSize =(int) pageSize
            };
            return View(result);

        }
        public IActionResult ShowMenu(int? id)
        {
           var menu= repository.GetById(id);
           var dto =MenuDto.FromEntity(mapper, menu);
           return View(dto);
        }

        [Route("Product/{id}/{title}")]
        public async Task<IActionResult> ShowProduct(int? id,string title,CancellationToken cancellationToken)
        {
            var model =await productService.GetProductById(id);
            model.Seen += 1;
            await productService.Update(model, cancellationToken);
            return View(model);
        }


        [HttpPost][Authorize]
        public async Task<IActionResult> AddToBasket(int productid,int productCount, CancellationToken cancellationToken)
        {
            if (User.Identity.IsAuthenticated)
            {
                await shoppingCartService.AddToCart(productid, productCount, User.Identity.Name, cancellationToken);
                return Redirect("/Profile/ShowShoppingCart");
            }
            return Redirect("/Login");
        }
    }
}
