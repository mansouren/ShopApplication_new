using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;

namespace ShopApplication.Pages.Admin.Brand
{
    public class CreateModel : PageModel
    {
        private readonly IBrandRepository repository;
        private readonly IMapper mapper;

        public CreateModel(IBrandRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [BindProperty]
        public BrandDto brandDto { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var model = brandDto.ToEntity(mapper);
                await repository.AddBrand(model, cancellationToken);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
