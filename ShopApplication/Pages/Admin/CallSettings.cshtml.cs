using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.Models;
using ShopApplication.Services;

namespace ShopApplication.Pages.Admin
{
    public class CallSettingsModel : PageModel
    {
        private readonly ISiteService siteService;
        private readonly IMapper mapper;

        [BindProperty]
        public CallSettingsDto callSettingsDto { get; set; }
        public CallSettingsModel(ISiteService siteService,IMapper mapper)
        {
            this.siteService = siteService;
            this.mapper = mapper;
        }
        public async void OnGet()
        {
            var callSettings = await siteService.GetSiteSetting();
            callSettingsDto = CallSettingsDto.FromEntity(mapper, callSettings);
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var model = callSettingsDto.ToEntity(mapper);
            await siteService.UpdateCallSettings(model, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
