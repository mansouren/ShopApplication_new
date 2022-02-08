using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApplication.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace ShopApplication.DataLayer.Entities
{
   public class Menu : BaseEntity
    {
        //[Display(Name = "نام منو")]
        public string Name { get; set; }

        //[Display(Name = "عدم نمایش")]
        public bool NotShow { get; set; }

        //[Display(Name = "ترتیب نمایش")]
        public int Order { get; set; }

        //[Display(Name = "محتویات")]
        //[DataType(DataType.MultilineText)]
        //[AllowHtml]
        public string Description { get; set; }
    }

    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(30);
            
        }
    }
}
