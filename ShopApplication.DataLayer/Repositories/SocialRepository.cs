using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Common;
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
    public class SocialRepository : Repository<SocialMedia>,ISocialRepository, IScopeDependency 
    {
        public SocialRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override Task AddAsync(SocialMedia entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            if(entity.SocialIconFile != null)
            {
                string extension = Path.GetExtension(entity.SocialIconFile.FileName);
                string filename = StringExtensions.GetFileName();
                entity.SocialIcon = filename + extension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/socialMedia/", entity.SocialIcon);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    entity.SocialIconFile.CopyToAsync(stream);
                }
                
            }
            SocialMedia socialMedia = new SocialMedia
            {
                SocialIcon = entity.SocialIcon,
                SocialLink = entity.SocialLink,
                SocialName = entity.SocialName,
                SocialOrder = entity.SocialOrder,
                NotShow = entity.NotShow
            };
            return base.AddAsync(socialMedia, cancellationToken, saveNow);
        }

        public override Task UpdateAsync(SocialMedia entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            if (entity.SocialIconFile != null)
            {
                if(entity.SocialIcon != null)
                {
                   File.Delete("wwwroot/Images/socialMedia/" + entity.SocialIcon);
                }
                string extension = Path.GetExtension(entity.SocialIconFile.FileName);
                string filename = StringExtensions.GetFileName();
                entity.SocialIcon = filename + extension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/socialMedia/", entity.SocialIcon);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    entity.SocialIconFile.CopyToAsync(stream);
                }
                return base.UpdateAsync(entity, cancellationToken);
            }
            return base.UpdateAsync(entity, cancellationToken, saveNow);
        }

        public override Task DeleteAsync(SocialMedia entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            if(entity.SocialIcon != null)
            {
                File.Delete("wwwroot/Images/socialMedia/" + entity.SocialIcon);
            }
            return base.DeleteAsync(entity, cancellationToken, saveNow);
        }
    }
    
}
