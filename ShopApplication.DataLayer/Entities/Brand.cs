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
   public class Brand : BaseEntity
    {
        public string Title { get; set; }
        public bool NotShow { get; set; }
        public int Order { get; set; }
        public string ImageName { get; set; }
        
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        #region Relations
        public virtual ICollection<Product> Products { get; set; }
        #endregion
    }

    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(b => b.Title).IsRequired().HasMaxLength(30);
            builder.Property(b => b.ImageName).HasMaxLength(100);
        }
    }
}
