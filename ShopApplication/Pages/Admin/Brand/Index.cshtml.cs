using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;

namespace ShopApplication.Pages.Admin.Brand
{
    public class IndexModel : PageModel
    {
        private readonly IBrandRepository repository;
        private readonly IMapper mapper;

        public IndexModel(IBrandRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [BindProperty]
        public IEnumerable<BrandDto> brandDtos { get; set; }
        public void OnGet()
        {
            brandDtos = repository.TableAsNoTracking.ProjectTo<BrandDto>(mapper.ConfigurationProvider).ToList();
                
        }
    }
}
