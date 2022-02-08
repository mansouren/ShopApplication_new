using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.DataLayer.Entities;
using AutoMapper;
using ShopApplication.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ShopApplication.Pages.Admin.Group
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<DataLayer.Entities.Group> repository;
        private readonly IMapper mapper;

        public IndexModel(IRepository<DataLayer.Entities.Group> repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [BindProperty]
        public IEnumerable<GroupDto> groupDtos { get; set; }
        public void OnGet()
        {
            groupDtos = repository.TableAsNoTracking
                       .ProjectTo<GroupDto>(mapper.ConfigurationProvider)
                       .OrderBy(g => g.Order).ToList();
        }
    }
}
