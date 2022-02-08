using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;

namespace ShopApplication.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository repository;
        private readonly IRepository<DataLayer.Entities.Group> groupRepo;
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;

        public CreateModel(IProductRepository repository,IRepository<DataLayer.Entities.Group> groupRepo
                           ,IBrandRepository brandRepository,IMapper mapper)
        {
            this.repository = repository;
            this.groupRepo = groupRepo;
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        [BindProperty]
        public ProductDto productDto { get; set; }
        public async void OnGet()
        {
            var group =await groupRepo.TableAsNoTracking.ToListAsync();
            ViewData["Group"] = new SelectList(group, "Id", "Name");
            var brands =await brandRepository.Table.ToListAsync();
            ViewData["brands"] = new SelectList(brands, "Id", "Title");
           
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var model = productDto.ToEntity(mapper);
                await repository.AddProduct(model, cancellationToken);
                return RedirectToPage("Index");
            }
            var group = await groupRepo.TableAsNoTracking.ToListAsync();
            ViewData["Group"] = new SelectList(group, "Id", "Name");
            var brands = await brandRepository.Table.ToListAsync();
            ViewData["brands"] = new SelectList(brands, "Id", "Title");
            return Page();
        }
    }
}
