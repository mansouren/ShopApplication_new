using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;
using ShopApplication.Services;

namespace ShopApplication.Pages.Admin
{
    
    public class SiteSettingsModel : PageModel
    {
        private ISiteService siteService;
        private readonly IMapper mapper;


        [BindProperty]
        public SettingsDto SettingsDto { get; set; }
        public SiteSettingsModel(ISiteService siteService,IMapper mapper)
        {
            this.siteService = siteService;
            this.mapper = mapper;
        }
        public async void OnGet()
        {
           var settings =await siteService.GetSiteSetting();
           SettingsDto = SettingsDto.FromEntity(mapper,settings);
        }

        public async Task<IActionResult> OnPost(SettingsDto settingsDto,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var model = settingsDto.ToEntity(mapper);
            await siteService.UpdateSettings(model, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
