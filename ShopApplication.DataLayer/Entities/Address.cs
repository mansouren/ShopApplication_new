using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApplication.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopApplication.DataLayer.Entities
{
    public class Address : BaseEntity
    {
        [Display(Name = "آدرس پستی")]
        [DataType(DataType.MultilineText)]
        public string AddressText { get; set; }
       
        [Display(Name = "شهر")]
        public int CityId { get; set; }
        

        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }

        #region Relations
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }

       
        #endregion

    }

    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.AddressText).IsRequired();
            builder.Property(a => a.CityId).IsRequired();
            builder.Property(a => a.PostalCode).IsRequired().HasMaxLength(10);
        }
    }


}
