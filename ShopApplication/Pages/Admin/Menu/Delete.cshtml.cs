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

namespace ShopApplication.Pages.Admin.Menu
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository<DataLayer.Entities.Menu> repository;
        private readonly IMapper mapper;

        public DeleteModel(IRepository<DataLayer.Entities.Menu> repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [BindProperty]
        public MenuDto MenuDto { get; set; }
        public void OnGet(int id,CancellationToken cancellationToken)
        {
            var model = repository.GetById(id);
            MenuDto = MenuDto.FromEntity(mapper, model);
        }

        public async Task<IActionResult> OnPost(int id,CancellationToken cancellationToken)
        {
            var model = repository.GetById(id);
            MenuDto.ToEntity(mapper, model);
            await repository.DeleteAsync(model, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
