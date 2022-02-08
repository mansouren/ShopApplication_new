using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;

namespace ShopApplication.Pages.Admin.Sliders
{
    public class CreateModel : PageModel
    {
        private readonly ISliderRepository sliderRepository;
        private readonly IMapper mapper;

        public CreateModel(ISliderRepository sliderRepository,IMapper mapper)
        {
            this.sliderRepository = sliderRepository;
            this.mapper = mapper;
        }
        [BindProperty]
        public SliderDto sliderDto { get; set; }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var model = sliderDto.ToEntity(mapper);
                await sliderRepository.AddSlider(model, cancellationToken);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
