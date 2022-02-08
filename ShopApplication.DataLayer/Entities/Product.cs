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
  public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int GroupId { get; set; }
        public int BrandId { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public bool NotShow { get; set; }
        public int Seen { get; set; }
        public string Img { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }
        public int Price { get; set; }

        #region Relations
        [ForeignKey(nameof(GroupId))]
        public virtual Group Group { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }

        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<ProductFeild> ProductFeilds { get; set; }
        public virtual ICollection<FactorDetail> FactorDetails { get; set; }
        #endregion
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Img).HasMaxLength(100);
            
        }
    }
}
