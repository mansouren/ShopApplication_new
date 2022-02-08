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
    public class DeleteModel : PageModel
    {
        private readonly IRepository<ProductFeild> repository;
        private readonly IProductService productService;

        public DeleteModel(IRepository<ProductFeild> repository,IProductService productService)
        {
            this.repository = repository;
            this.productService = productService;
        }
        [BindProperty]
        public ProductFeild productFeild { get; set; }
        public async Task<IActionResult> OnGet(int id,int productid, CancellationToken cancellationToken)
        {
            ViewData["ProductId"] = new SelectList(await productService.GetProducts(productid, cancellationToken), "Id", "Name");
            productFeild =await repository.GetByIdAsync(cancellationToken, id);
            await repository.DeleteAsync(productFeild, cancellationToken);
            return Redirect("/Admin/ProductField/Index/" + productid);

        }
    }
}
