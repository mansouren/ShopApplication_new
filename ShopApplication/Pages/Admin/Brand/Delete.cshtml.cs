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
    public class DeleteModel : PageModel
    {
        private readonly IBrandRepository repository;
        private readonly IMapper mapper;

        public DeleteModel(IBrandRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [BindProperty]
        public BrandDto brandDto { get; set; }
        public async void OnGet(int id,CancellationToken cancellationToken)
        {
            var model =await repository.GetByIdAsync(cancellationToken, id);
            brandDto = BrandDto.FromEntity(mapper, model);
        }


        public async Task<IActionResult> OnPost(int id,CancellationToken cancellationToken)
        {
            var model = await repository.GetByIdAsync(cancellationToken, id);
            brandDto.ToEntity(mapper, model);
            await repository.DeleteBrand(model, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
