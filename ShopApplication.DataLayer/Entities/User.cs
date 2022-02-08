using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApplication.DataLayer.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApplication.DataLayer.Entities
{
    public class User : BaseEntity
    {
        [Display(Name = "شماره موبایل")]
        public string Mobile { get; set; }
        
        [Display(Name = "پسورد")]
        public string Password { get; set; }
       
        [Display(Name = "کد فعالساز")]
        public string UserCode { get; set; }

        [Display(Name = "فعالسازی")]
        public bool IsActive { get; set; }

        public string RefreshToken { get; set; }

        [Display(Name ="نقش کاربر")]
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Factor> Factors { get; set; }
    }

    //Using FluentApi for Attributes
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserCode).IsRequired().HasMaxLength(6);
            builder.Property(u => u.Mobile).IsRequired().HasMaxLength(11);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(150);
            builder.Property(u => u.RefreshToken).HasMaxLength(50);
        }
    }

   
}
