using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Common;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.DataLayer.Repositories
{
    public class GalleryRepository : Repository<Gallery>, IGalleryRepositroy, IScopeDependency
    {

        public GalleryRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task AddGallery(Gallery gallery, CancellationToken cancellationToken)
        {
            if (gallery.ImgFile != null)
            {
                string extension = Path.GetExtension(gallery.ImgFile.FileName);
                string filename = StringExtensions.GetFileName();
                gallery.Img = filename + extension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Gallery/", gallery.Img);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await gallery.ImgFile.CopyToAsync(stream);
                }
            }
            await base.AddAsync(gallery, cancellationToken);
        }

        public async Task DeleteGallery(Gallery gallery, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(gallery.Img))
            {
                string deletepath = Path.Combine("wwwroot/Images/Gallery/", gallery.Img);
                if(File.Exists(deletepath))
                {
                    File.Delete(deletepath);
                }
            }
            //Entities.Attach(gallery);
            await base.DeleteAsync(gallery, cancellationToken);
            
        }

        public List<Gallery> GetGalleriesbyProductId(int id)
        {
            var gallerylst = Table.Include(p => p.Product)
                          .Where(g => g.ProductId == id).ToList();
            return gallerylst;
        }

        public Gallery GetGalleryById(int id)
        {
            var gallery = Table.Include(p => p.Product).SingleOrDefault(p => p.Id == id);
            return gallery;
        }
    }
}
