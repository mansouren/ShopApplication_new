using Microsoft.AspNetCore.Mvc;
using ShopApplication.DataLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.ViewComponents
{
    public class GalleryComponent : ViewComponent
    {
        private readonly IGalleryRepositroy galleryRepositroy;

        public GalleryComponent(IGalleryRepositroy galleryRepositroy)
        {
            this.galleryRepositroy = galleryRepositroy;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var galleries = galleryRepositroy.GetGalleriesbyProductId(id);
            return await Task.FromResult((IViewComponentResult)View("Galleries", galleries));
        }
    }
}
