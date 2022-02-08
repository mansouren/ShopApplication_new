
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;

namespace ShopApplication.Pages.Admin.Sliders
{
    public class EditModel : PageModel
    {
        private readonly ISliderRepository sliderRepository;
        private readonly IMapper mapper;

        public EditModel(ISliderRepository sliderRepository, IMapper mapper)
        {
            this.sliderRepository = sliderRepository;
            this.mapper = mapper;
        }
        [BindProperty]
        public SliderDto sliderDto { get; set; }
        public async void OnGet(int id,CancellationToken cancellationToken)
        {
            var model =await sliderRepository.GetByIdAsync(cancellationToken, id);
            sliderDto = SliderDto.FromEntity(mapper, model);
        }

        public async Task<IActionResult> OnPost(int id,CancellationToken cancellationToken)
        {
            var model = await sliderRepository.GetByIdAsync(cancellationToken, id);
            if (ModelState.IsValid)
            {
                sliderDto.ToEntity(mapper, model);
                await sliderRepository.UpdateSlider(model, cancellationToken);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
