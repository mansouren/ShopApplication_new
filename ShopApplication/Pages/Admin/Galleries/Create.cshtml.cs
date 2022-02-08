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

namespace ShopApplication.Pages.Admin.Galleries
{
    public class CreateModel : PageModel
    {
        private readonly IGalleryRepositroy repositroy;
        private readonly IProductService productService;

        public CreateModel(IGalleryRepositroy repositroy, IProductService productService)
        {
            this.repositroy = repositroy;
            this.productService = productService;
        }

        [BindProperty]
        public Gallery Gallery { get; set; }
        public async void OnGet(int id, CancellationToken cancellationToken)
        {
            ViewData["product"] = new SelectList(await productService.GetProducts(id, cancellationToken), "Id", "Name");

        }

        
        public async Task<IActionResult> OnPost(int id, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await repositroy.AddGallery(Gallery, cancellationToken);
                return Redirect("/Admin/Galleries/Index/"+ id);
            }
            ViewData["product"] = new SelectList(await productService.GetProducts(id, cancellationToken), "Id", "Name");
            return Page();
        }
    }
}
