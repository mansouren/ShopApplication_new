using ShopApplication.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.DataLayer.Repositories.Contracts
{
   public interface IGalleryRepositroy : IRepository<Gallery>
    {
        List<Gallery> GetGalleriesbyProductId(int id);
        Task AddGallery(Gallery gallery, CancellationToken cancellationToken);
        Task DeleteGallery(Gallery gallery, CancellationToken cancellationToken);
        Gallery GetGalleryById(int id);
    }
}
