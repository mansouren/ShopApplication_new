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
    public class DeleteModel : PageModel
    {
        private readonly IRepository<Feild> repository;

        public DeleteModel(IRepository<Feild> repository)
        {
            this.repository = repository;
        }

        [BindProperty]
        public Feild feild { get; set; }
        public async Task<IActionResult> OnGet(int id,CancellationToken cancellationToken)
        {
            feild = await repository.GetByIdAsync(cancellationToken, id);
            await repository.DeleteAsync(feild, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
