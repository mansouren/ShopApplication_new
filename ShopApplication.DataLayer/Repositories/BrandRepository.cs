using Common.Utilities;
using ShopApplication.Common;
using ShopApplication.Common.Utilities;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using System;

using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.DataLayer.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository, IScopeDependency
    {
        public BrandRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task AddBrand(Brand brand, CancellationToken cancellationToken)
        {
            if(brand.ImageFile != null)
            {
                string extension = Path.GetExtension(brand.ImageFile.FileName);
                string filename = StringExtensions.GetFileName();
                brand.ImageName = filename + extension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Brands/", brand.ImageName);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                   await brand.ImageFile.CopyToAsync(stream);
                }
                #region thumb
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Brands/thumb/", brand.ImageName);
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(path, thumbPath, 100);
                
                #endregion
                await base.AddAsync(brand, cancellationToken);
            }
        }

        public async Task DeleteBrand(Brand brand, CancellationToken cancellationToken)
        {
            if(brand.ImageName != string.Empty)
            {
                string deletPath = Path.Combine("wwwroot/Images/Brands/", brand.ImageName);
                if (File.Exists(deletPath))
                {
                    File.Delete(deletPath);
                }
                string deleteThumbPath = Path.Combine("wwwroot/Images/Brands/thumb/", brand.ImageName);
                if (File.Exists(deleteThumbPath))
                {
                    File.Delete(deleteThumbPath);
                }
            }
            await base.DeleteAsync(brand, cancellationToken);
        }

        public async Task UpdateBrand(Brand brand, CancellationToken cancellationToken)
        {
            if(brand.ImageFile != null)
            {
                if(brand.ImageName != string.Empty)
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Brands/", brand.ImageName);
                    if (File.Exists(deletePath))
                        File.Delete(deletePath);
                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Brands/thumb/", brand.ImageName);
                    if (File.Exists(deletethumbPath))
                        File.Delete(deletethumbPath);
                }
                string extension = Path.GetExtension(brand.ImageFile.FileName);
                string filename = StringExtensions.GetFileName();
                brand.ImageName = filename + extension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Brands/", brand.ImageName);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                   await brand.ImageFile.CopyToAsync(stream);
                }
                #region thumb
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Brands/thumb/", brand.ImageName);
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(path, thumbPath, 100);
                #endregion
            }
            await base.UpdateAsync(brand, cancellationToken);
        }
    }
}
