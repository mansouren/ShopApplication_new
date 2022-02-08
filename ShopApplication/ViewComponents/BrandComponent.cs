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
    public class BrandComponent : ViewComponent
    {
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;

        public BrandComponent(IBrandRepository brandRepository,IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brandDtolst =await brandRepository.TableAsNoTracking.ProjectTo<BrandDto>(mapper.ConfigurationProvider)
                .Where(b => b.NotShow == false).OrderBy(b => b.Order).ToListAsync();
            return await Task.FromResult((IViewComponentResult)View("Brands", brandDtolst));
        }
    }
}
