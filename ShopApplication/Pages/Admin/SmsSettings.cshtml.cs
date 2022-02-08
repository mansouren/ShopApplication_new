using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.Common;
using ShopApplication.Models;
using ShopApplication.Services;

namespace ShopApplication.Pages.Admin
{
    
    public class SmsSettingsModel : PageModel
    {
        private readonly ISiteService siteService;
        private readonly IMapper mapper;

        public SmsSettingsModel(ISiteService siteService,IMapper mapper)
        {
            this.siteService = siteService;
            this.mapper = mapper;
        }

        [BindProperty]
        public SmsSettingsDto smsSettingsDto { get; set; }
        public async void OnGet()
        {
            var model = await siteService.GetSiteSetting();
            smsSettingsDto = SmsSettingsDto.FromEntity(mapper, model);
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var model = smsSettingsDto.ToEntity(mapper);
            await siteService.UpdateSmsSettings(model, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
