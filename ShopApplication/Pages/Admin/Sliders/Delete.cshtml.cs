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
    public class DeleteModel : PageModel
    {
        private readonly ISliderRepository sliderRepository;
        private readonly IMapper mapper;

        public DeleteModel(ISliderRepository sliderRepository, IMapper mapper)
        {
            this.sliderRepository = sliderRepository;
            this.mapper = mapper;
        }
        [BindProperty]
        public SliderDto sliderDto { get; set; }
        public async void OnGet(int id,CancellationToken cancellationToken)
        {
            var slider =await sliderRepository.GetByIdAsync(cancellationToken, id);
            sliderDto = SliderDto.FromEntity(mapper, slider);
        }

        public async Task<IActionResult> OnPost(int id,CancellationToken cancellationToken)
        {
            var model = await sliderRepository.GetByIdAsync(cancellationToken, id);
            sliderDto.ToEntity(mapper, model);
            await sliderRepository.DeleteSlider(model, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
