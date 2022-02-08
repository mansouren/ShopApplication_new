using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.ViewComponents
{
    public class GroupComponent : ViewComponent
    {
        private readonly IRepository<Group> repository;
        private readonly IMapper mapper;

        public GroupComponent(IRepository<Group> repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var grouplst = repository.TableAsNoTracking.ProjectTo<GroupDto>(mapper.ConfigurationProvider)
                           .Where(g => g.NotShow == false)
                           .OrderBy(g => g.Order).ToList();
            return await Task.FromResult((IViewComponentResult)View("Groups", grouplst));
        }
    }
}
