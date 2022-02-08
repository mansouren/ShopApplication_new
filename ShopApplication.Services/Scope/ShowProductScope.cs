using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services.Scope
{
   public class ShowProductScope
    {
        private readonly IGalleryRepositroy repositroy;

        public ShowProductScope(IGalleryRepositroy repositroy)
        {
            this.repositroy = repositroy;
        }

        public List<Gallery> GetGalleries(int productid)
        {
            return repositroy.GetGalleriesbyProductId(productid);
        }
    }
}
