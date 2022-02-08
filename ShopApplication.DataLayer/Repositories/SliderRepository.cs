using Common.Utilities;
using ShopApplication.Common;
using ShopApplication.Common.Utilities;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.DataLayer.Repositories
{
    public class SliderRepository : Repository<Slider>, ISliderRepository, IScopeDependency
    {
        public SliderRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task AddSlider(Slider slider, CancellationToken cancellationToken)
        {
            string extension = Path.GetExtension(slider.ImageFile.FileName);
            string filename = StringExtensions.GetFileName();
            slider.Imgname = filename + extension;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slide/", slider.Imgname);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await slider.ImageFile.CopyToAsync(stream);
            }

            #region Thumbnail
            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slide/thumb", slider.Imgname);
            ImageConvertor imageConvertor = new ImageConvertor();
            imageConvertor.Image_resize(path, thumbPath, 100);
            #endregion
            await base.AddAsync(slider, cancellationToken);
        }

        public async Task UpdateSlider(Slider slider, CancellationToken cancellationToken)
        {
            if (slider.ImageFile != null)
            {
                #region DeleteImgName
                string deletPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slide/", slider.Imgname);
                if (File.Exists(deletPath))
                {
                    File.Delete(deletPath);
                }
                string deleteThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slide/thumb", slider.Imgname);
                if (File.Exists(deleteThumbPath))
                {
                    File.Delete(deleteThumbPath);
                }
                #endregion

                string extension = Path.GetExtension(slider.ImageFile.FileName);
                string filename = StringExtensions.GetFileName();
                slider.Imgname = filename + extension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slide/", slider.Imgname);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await slider.ImageFile.CopyToAsync(stream);
                }
                #region Thumbnail
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slide/thumb", slider.Imgname);
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(path, thumbPath, 100);
                #endregion
            }
            
            await base.UpdateAsync(slider, cancellationToken);
        }

        public async Task DeleteSlider(Slider slider, CancellationToken cancellationToken)
        {
            if (slider.Imgname != null)
            {
                string deletPath = Path.Combine("wwwroot/Images/Slide/", slider.Imgname);
                if (File.Exists(deletPath))
                {
                    File.Delete(deletPath);
                }
                string deleteThumbPath = Path.Combine("wwwroot/Images/Slide/thumb", slider.Imgname);
                if (File.Exists(deleteThumbPath))
                {
                    File.Delete(deleteThumbPath);
                }
                
            }
            await base.DeleteAsync(slider, cancellationToken);
        }
    }
}
