using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Common.Utilities.Common.Utilities;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;
using ShopApplication.Services;
using ShopApplication.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.Controllers
{
    public class AddressController : Controller
    {
        private readonly IRepository<Address> repository;
        private readonly IRepository<City> cityRepository;
        private readonly IRepository<State> stateRepositroy;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public AddressController(IRepository<Address> repository, IRepository<City> cityRepository,
                                 IRepository<State> stateRepositroy, IUserService userService, IMapper mapper)

        {
            this.repository = repository;
            this.cityRepository = cityRepository;
            this.stateRepositroy = stateRepositroy;
            this.userService = userService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var mobile = User.Identity.FindFirstValue(ClaimTypes.Name);
            var user = await userService.GetUserByMobile(mobile);
            if (user.IsActive == false)
            {
                return RedirectToAction("Activate", "Account");
            }
            var addresses = await repository.TableAsNoTracking.Include(x => x.City)
                           .ThenInclude(x => x.State).Where(x => x.UserId == user.Id).ToListAsync();
           
            return View(addresses);
        }

        public IActionResult Create()
        {
            ViewBag.Statelst = new SelectList(GetStates(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddressDto addressDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Statelst = new SelectList(GetStates(), "Id", "Name");
                return View();
            }

            var user = await userService.GetUserByMobile(User.Identity.Name);
            var model = addressDto.ToEntity(mapper);
            model.UserId = user.Id;
            await repository.AddAsync(model, cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var address = await repository.GetByIdAsync(cancellationToken, id);
            ViewBag.Statelst = new SelectList(GetStates(), "Id", "Name", address.City.StateId);
            ViewBag.Citylst = new SelectList(GetCitiesbySelectedStateId(address.City.StateId), "Id", "Name",address.CityId);
            var dto = AddressDto.FromEntity(mapper, address);
            dto.StateId = address.City.StateId;
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddressDto dto, CancellationToken cancellationToken)
        {
            var model = await repository.GetByIdAsync(cancellationToken, id);
            if (!ModelState.IsValid)
            {
                ViewBag.Statelst = new SelectList(GetStates(), "Id", "Name", model.City.StateId);
                ViewBag.Citylst = new SelectList(GetCitiesbySelectedStateId(model.City.StateId), "Id", "Name", model.CityId);
                return View();
            }
            
            var address = dto.ToEntity(mapper, model);
            await repository.UpdateAsync(address, cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var model = await repository.GetByIdAsync(cancellationToken, id);
            await repository.DeleteAsync(model, cancellationToken);
            return RedirectToAction("Index");
        }

        public List<State> GetStates()
        {
            var lst = stateRepositroy.Table.Include(x => x.Cities).ToList();
            return lst;
        }

        public JsonResult GetCities(int id)
        {
            var cities = cityRepository.Table.Where(x => x.StateId == id).ToList();
            var data = new SelectList(cities, "Id", "Name");
            return Json(data);
        }

        public List<City> GetCitiesbySelectedStateId(int stateId)
        {
            return cityRepository.Table.Where(x => x.StateId == stateId).ToList();
        }
    }
}
