using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopApplication.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductRepository repository;
        private readonly IRepository<DataLayer.Entities.Group> groupRepo;
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;

        public EditModel(IProductRepository repository, IRepository<DataLayer.Entities.Group> groupRepo
                         ,IBrandRepository brandRepository,IMapper mapper)
        {
            this.repository = repository;
            this.groupRepo = groupRepo;
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        [BindProperty]
        public ProductDto productDto { get; set; }

        public async void OnGet(int id, CancellationToken cancellationToken)
        {
            var model =await repository.GetByIdAsync(cancellationToken, id);
            productDto = ProductDto.FromEntity(mapper, model);
            var group =await groupRepo.TableAsNoTracking.ToListAsync();
            ViewData["Group"] = new SelectList(group, "Id", "Name", productDto.GroupId);
            var brands =await brandRepository.TableAsNoTracking.ToListAsync();
            ViewData["brands"] = new SelectList(brands, "Id", "Title",productDto.BrandId);
        }

        public async Task<IActionResult> OnPost(int id,CancellationToken cancellationToken)
        {
            var model = await repository.GetByIdAsync(cancellationToken, id);
            if (ModelState.IsValid)
            {
                productDto.ToEntity(mapper, model);
                await repository.UpdateProduct(model, cancellationToken);
                return RedirectToPage("Index");

            }
            var group = await groupRepo.TableAsNoTracking.ToListAsync();
            ViewData["Group"] = new SelectList(group, "Id", "Name", productDto.GroupId);
            var brands = await brandRepository.TableAsNoTracking.ToListAsync();
            ViewData["brands"] = new SelectList(brands, "Id", "Title", productDto.BrandId);
            return Page();
        }
    }
}
