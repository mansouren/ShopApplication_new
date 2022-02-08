using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Services.Contracts;

namespace ShopApplication.Pages.Admin.ProductField
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<ProductFeild> repository;
        
        public IndexModel(IRepository<ProductFeild> repository)
        {
            this.repository = repository;
            
        }

        [BindProperty]
        public IEnumerable<ProductFeild> productFields { get; set; }
        public void OnGet(int id)
        {
            productFields = repository.TableAsNoTracking.Include(x => x.Feild).ToList();
            ViewData["ProductId"] = id;
        }
    }
}
