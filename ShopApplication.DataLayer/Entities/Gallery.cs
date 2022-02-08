using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApplication.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopApplication.DataLayer.Entities
{
   public class Gallery : BaseEntity
    {
        public int ProductId { get; set; }

        public string Img { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }

        #region Relations
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        #endregion
    }

    public class GalleryConfiguration : IEntityTypeConfiguration<Gallery>
    {
        public void Configure(EntityTypeBuilder<Gallery> builder)
        {
            builder.Property(g => g.Img).HasMaxLength(100);
        }
    }
}
