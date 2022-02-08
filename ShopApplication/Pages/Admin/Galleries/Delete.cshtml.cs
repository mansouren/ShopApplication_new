using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Services.Contracts;

namespace ShopApplication.Pages.Admin.Galleries
{
    public class DeleteModel : PageModel
    {
        private readonly IGalleryRepositroy galleryRepositroy;
       

        public DeleteModel(IGalleryRepositroy galleryRepositroy)
        {
            this.galleryRepositroy = galleryRepositroy;
           
        }

        [BindProperty]
        public Gallery Gallery { get; set; }
        public async Task<IActionResult> OnGet(int productid,int id, CancellationToken cancellationToken)
        {
            Gallery =  galleryRepositroy.GetGalleryById(id);
            await galleryRepositroy.DeleteGallery(Gallery, cancellationToken);
            return Redirect("/Admin/Galleries/Index/" + productid);
        }

     
    }
}
