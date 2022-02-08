using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;

namespace ShopApplication.Pages.Admin.Social
{ 
    
    public class CreateModel : PageModel
    {
        private readonly ISocialRepository repository;
       
        public CreateModel(ISocialRepository repository)
        {
            this.repository = repository;
            
        }
        [BindProperty]
        public SocialMedia socialMedia { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await repository.AddAsync(socialMedia, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
