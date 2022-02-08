using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;
using ShopApplication.Services;
using ShopApplication.Services.Contracts;

namespace ShopApplication.Pages.Admin.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserService userService;
        private readonly IRepository<Role> repository;
        private readonly IMapper mapper;

        public CreateModel(IUserService userService,IRepository<Role> repository,IMapper mapper)
        {
            this.userService = userService;
            this.repository = repository;
            this.mapper = mapper;
        }
        [BindProperty]
        public UserDto userDto { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = new SelectList(repository.TableAsNoTracking.ToList(), "Id", "RoleTitle");
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                bool exist = await userService.IsExistMobile(userDto.Mobile);
                if (exist)
                {
                    ModelState.AddModelError(userDto.Mobile, "این شماره موبایل قبلا ثبت شده است");
                }
                else
                {
                    var model = userDto.ToEntity(mapper);
                    await userService.AddUser(model, cancellationToken);
                    return RedirectToPage("Index");
                }
            }
            ViewData["Roles"] =new SelectList(repository.TableAsNoTracking.ToList(),"Id", "RoleTitle");
            return Page();
        }
    }
}
