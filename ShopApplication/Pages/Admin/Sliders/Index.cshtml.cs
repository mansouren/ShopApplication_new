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

namespace ShopApplication.Pages.Admin.Sliders
{
    public class IndexModel : PageModel
    {
        private readonly ISliderRepository repository;
        private readonly IMapper mapper;

        public IndexModel(ISliderRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [BindProperty]
        public IEnumerable<SliderDto> SliderDtos { get; set; }
        public void OnGet()
        {
           SliderDtos = repository.TableAsNoTracking.ProjectTo<SliderDto>(mapper.ConfigurationProvider).ToList();

        }
    }
}
