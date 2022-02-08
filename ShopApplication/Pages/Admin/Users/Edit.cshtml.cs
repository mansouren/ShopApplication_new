using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;
using ShopApplication.Services;
using ShopApplication.Services.Contracts;

namespace ShopApplication.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        private readonly IUserService userService;
        private readonly IRepository<Role> roleRepository;
        private readonly IMapper mapper;

        public EditModel(IUserService userService,IRepository<Role> roleRepository,IMapper mapper)
        {
            this.userService = userService;
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }
        
        [BindProperty]
        public UserDto userDto { get; set; }
        public async void OnGet(int id,CancellationToken cancellationToken)
        {
           var model = await userService.GetUserById(id, cancellationToken);
           userDto = UserDto.FromEntity(mapper, model);
           ViewData["Roles"] = new SelectList(await roleRepository.TableAsNoTracking.ToListAsync(),"Id",
                                               "RoleTitle", model.RoleId);
        }

        public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
        {
            var model = await userService.GetUserById(id, cancellationToken);
            if (ModelState.IsValid && model.Mobile != User.Identity.Name)
            {
                userDto.ToEntity(mapper, model);
                await userService.UpdateUser(model, cancellationToken);
                return RedirectToPage("Index");
            }
            if(ModelState.IsValid && model.Mobile == User.Identity.Name)
            {
                ModelState.AddModelError("Mobile", "شما با این شماره همراه وارد شده اید،لطفا برای تغییر با حساب کاربری دیگری که دسترسی ادمین دارد، وارد شوید");

            }

            ViewData["Roles"] = new SelectList(await roleRepository.TableAsNoTracking.ToListAsync(), "Id",
                                               "RoleTitle", model.RoleId);
                return Page();
           
            
        }

    }
}
