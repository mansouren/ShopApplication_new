using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;

namespace ShopApplication.Pages.Admin.Social
{
    //[AllowAnonymous]
    public class IndexModel : PageModel
    {
        
        private readonly ISocialRepository socialRepository;

        public IndexModel(ISocialRepository socialRepository)
        {
            
            this.socialRepository = socialRepository;
        }

        [BindProperty]
        public List<SocialMedia> socialMedias { get; set; }
        public void OnGet()
        {
            socialMedias = socialRepository.TableAsNoTracking.ToList();
            
        }
    }
}
