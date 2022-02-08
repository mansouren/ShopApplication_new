using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;

namespace ShopApplication.Pages.Admin.Galleries
{
    public class IndexModel : PageModel
    {
        private readonly IGalleryRepositroy repositroy;

        public IndexModel(IGalleryRepositroy repositroy)
        {
            this.repositroy = repositroy;
        }

        [BindProperty]
        public IEnumerable<Gallery> Galleries { get; set; }
       
        public  void OnGet(int id)
        {
            Galleries = repositroy.GetGalleriesbyProductId(id);
            ViewData["ProductId"] = id;
        }
    }
}
