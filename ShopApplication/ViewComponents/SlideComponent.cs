using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.ViewComponents
{
    public class SlideComponent : ViewComponent
    {
        private readonly ISliderRepository sliderRepository;
        private readonly IMapper mapper;

        public SlideComponent(ISliderRepository sliderRepository, IMapper mapper)
        {
            this.sliderRepository = sliderRepository;
            this.mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliderDtolst = await sliderRepository.TableAsNoTracking
                              .ProjectTo<SliderDto>(mapper.ConfigurationProvider)
                              .Where(s => s.NotShow == false).OrderBy(s => s.Order).ToListAsync();
            return await Task.FromResult((IViewComponentResult)View("Slider",sliderDtolst));

        }
    }
}
