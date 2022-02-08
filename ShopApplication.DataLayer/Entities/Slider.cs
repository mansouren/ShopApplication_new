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
   public class Slider : BaseEntity
    {
        public string Title { get; set; }
        public string Imgname { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public int Order { get; set; }
        public bool NotShow { get; set; }

    }

    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(s => s.Imgname).HasMaxLength(100);
            builder.Property(s => s.Title).HasMaxLength(100);
        }
    }
}
