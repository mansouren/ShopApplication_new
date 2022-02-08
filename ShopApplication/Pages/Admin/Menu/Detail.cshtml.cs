using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;

namespace ShopApplication.Pages.Admin.Menu
{
    //[AllowAnonymous]
    public class DetailModel : PageModel
    {
        private readonly IRepository<DataLayer.Entities.Menu> repository;
        private readonly IMapper mapper;

        public DetailModel(IRepository<DataLayer.Entities.Menu> repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [BindProperty]
        public MenuDto MenuDto { get; set; }
        public async void OnGet(CancellationToken cancellationToken,int id)
        {
            var model =await repository.GetByIdAsync(cancellationToken, id);
            MenuDto = MenuDto.FromEntity(mapper, model);
        }

    }
}
