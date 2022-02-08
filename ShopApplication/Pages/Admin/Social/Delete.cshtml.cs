using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;

namespace ShopApplication.Pages.Admin.Social
{
    public class DeleteModel : PageModel
    {
        private readonly ISocialRepository socialRepository;

        public DeleteModel(ISocialRepository socialRepository)
        {
            this.socialRepository = socialRepository;
        }
       
        [BindProperty]
        public SocialMedia socialMedia { get; set; }
        
        public async void OnGet(int id, CancellationToken cancellationToken)
        {
            socialMedia = await socialRepository.GetByIdAsync(cancellationToken,id);
             
        }

        public async Task<IActionResult> OnPost(int id, CancellationToken cancellationToken)
        {
            await socialRepository.DeleteAsync(socialMedia, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
