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

namespace ShopApplication.Pages.Admin.Group
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<DataLayer.Entities.Group> repository;
        private readonly IMapper mapper;

        public CreateModel(IRepository<DataLayer.Entities.Group> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [BindProperty]
        public GroupDto groupDto { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var model = groupDto.ToEntity(mapper);
                await repository.AddAsync(model, cancellationToken);
                return RedirectToPage("Index");
            }
            return Page();
        }
        
    }
}
