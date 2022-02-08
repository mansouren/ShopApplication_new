using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.ViewComponents
{
    public class FieldComponent : ViewComponent
    {
        private readonly IRepository<ProductFeild> repository;

        public FieldComponent(IRepository<ProductFeild> repository)
        {
            this.repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int productid)
        {
            var fields =await repository.TableAsNoTracking.Include(x => x.Feild).Where(x => x.ProductId == productid).ToListAsync();
            return await Task.FromResult((IViewComponentResult)View("ShowFields", fields));
        }
    }
}
