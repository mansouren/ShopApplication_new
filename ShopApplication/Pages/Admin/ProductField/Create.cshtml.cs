using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Services.Contracts;

namespace ShopApplication.Pages.Admin.ProductField
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<ProductFeild> repository;
        private readonly IProductService productService;
        private readonly IRepository<Feild> feildrepository;

        public CreateModel(IRepository<ProductFeild> repository,IProductService productService,IRepository<Feild> feildrepository)
        {
            this.repository = repository;
            this.productService = productService;
            this.feildrepository = feildrepository;
        }

        [BindProperty]
        public ProductFeild productFeild { get; set; }
        
        public async void OnGet(int id,CancellationToken cancellationToken)
        {
            ViewData["ProductId"] = new SelectList(await productService.GetProducts(id, cancellationToken), "Id", "Name");
            ViewData["Fields"] = new SelectList(feildrepository.TableAsNoTracking.ToList(), "Id", "Name");
        }

        public async Task<IActionResult> OnPost(int id, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
               await repository.AddAsync(productFeild, cancellationToken);
                return Redirect("/Admin/ProductField/Index/" + id);
            }

            ViewData["ProductId"] =new SelectList(await productService.GetProducts(id, cancellationToken),"Id", "Name");
            ViewData["Fields"] = new SelectList(feildrepository.TableAsNoTracking.ToList(), "Id", "Name");
            return Page();
        }
    }
}
