using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;

namespace ShopApplication.Pages.Admin.Fields
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<Feild> repository;

        public CreateModel(IRepository<Feild> repository)
        {
            this.repository = repository;
        }
       
        [BindProperty]
        public Feild feild { get; set; }
        
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await repository.AddAsync(feild, cancellationToken);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
