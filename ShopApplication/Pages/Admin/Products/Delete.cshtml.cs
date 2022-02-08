using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;

namespace ShopApplication.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductRepository productRepository;

        public DeleteModel(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [BindProperty]
        public Product product { get; set; }
        public async void OnGet(int id, CancellationToken cancellationToken)
        {
            product =await productRepository.GetByIdAsync(cancellationToken, id);
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            await productRepository.DeleteProduct(product, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
