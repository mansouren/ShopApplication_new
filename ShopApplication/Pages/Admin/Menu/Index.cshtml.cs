using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;

namespace ShopApplication.Pages.Admin.Menu
{
    //[AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly IRepository<DataLayer.Entities.Menu> repository;
        private readonly IMapper mapper;

        public IndexModel(IRepository<DataLayer.Entities.Menu> repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [BindProperty]
        public List<MenuDto> MenuDtos { get; set; }
        public void OnGet()
        {
            MenuDtos = repository.TableAsNoTracking.ProjectTo<MenuDto>(mapper.ConfigurationProvider)
                       .ToList();
        }

     
    }
}
