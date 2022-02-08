using Microsoft.AspNetCore.Mvc;
using ShopApplication.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.ViewComponents
{
    public class SuggestionProductsComponent : ViewComponent
    {
        private readonly IProductService service;

        public SuggestionProductsComponent(IProductService service)
        {
            this.service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id,int? brandid,int? groupid)
        {
            var products = await service.GetSuggestionProducts(id, brandid, groupid);
            return await Task.FromResult((IViewComponentResult)View("SuggestionProducts", products));
        }

    }
}
