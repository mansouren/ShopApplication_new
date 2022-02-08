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
    public class EditModel : PageModel
    {
        private readonly IRepository<Feild> repository;

        public EditModel(IRepository<Feild> repository)
        {
            this.repository = repository;
        }

        [BindProperty]
        public Feild feild { get; set; }

        public async void OnGet(int id, CancellationToken cancellationToken)
        {
            feild =await repository.GetByIdAsync(cancellationToken,id);
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await repository.UpdateAsync(feild, cancellationToken);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
