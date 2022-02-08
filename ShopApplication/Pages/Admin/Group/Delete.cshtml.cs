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
    public class DeleteModel : PageModel
    {
        private readonly IRepository<DataLayer.Entities.Group> repository;
        private readonly IMapper mapper;

        public DeleteModel(IRepository<DataLayer.Entities.Group> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [BindProperty]
        public GroupDto groupDto { get; set; }
        public async void OnGet(int id, CancellationToken cancellationToken)
        {
            var model =await repository.GetByIdAsync(cancellationToken, id);
            groupDto = GroupDto.FromEntity(mapper, model);
        }

        public async Task<IActionResult> OnPost(int id,CancellationToken cancellationToken)
        {
            var model = await repository.GetByIdAsync(cancellationToken, id);
            groupDto.ToEntity(mapper, model);
            await repository.DeleteAsync(model, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
