using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.ViewComponents
{
    public class MostSeenProductsComponent : ViewComponent
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public MostSeenProductsComponent(IProductRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mostseenProductDtos =await repository.TableAsNoTracking
                                     .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
                                     .Where(p => p.NotShow == false)
                                     .OrderByDescending(p => p.Seen).Take(4).ToListAsync();
            return await Task.FromResult((IViewComponentResult)View("MostSeen", mostseenProductDtos));
        }
    }
}
