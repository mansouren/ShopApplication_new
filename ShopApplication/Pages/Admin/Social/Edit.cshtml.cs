using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;


namespace ShopApplication.Pages.Admin.Social
{
   
    public class EditModel : PageModel
    {
        private readonly ISocialRepository repository;
        
        public EditModel(ISocialRepository repository)
        {
            this.repository = repository;
            
        }

        [BindProperty]
        public SocialMedia socialMedia { get; set; }
        public async void OnGet(int id,CancellationToken cancellationToken)
        {
            socialMedia =await repository.GetByIdAsync(cancellationToken,id);
            
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await repository.UpdateAsync(socialMedia, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
