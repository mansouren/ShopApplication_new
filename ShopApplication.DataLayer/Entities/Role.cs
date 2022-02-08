using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApplication.DataLayer.Entities.Common;

namespace ShopApplication.DataLayer.Entities
{
  public  class Role : BaseEntity
    {
      
        [Display(Name ="نام نقش")]
        public string RoleName { get; set; }

        [Display(Name = "عنوان نقش")]
        public string RoleTitle { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

    public class RoleValidation : AbstractValidator<Role>
    {
        public RoleValidation()
        {
            RuleFor(r => r.RoleName).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                .MaximumLength(20).WithMessage("نام نقش نمی تواند بیشتر از 20 کاراکتر باشد");
            
            RuleFor(r => r.RoleTitle).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
               .MaximumLength(20).WithMessage("عنوان نقش نمی تواند بیشتر از 20 کاراکتر باشد");

        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.RoleName).IsRequired().HasMaxLength(20);
            builder.Property(r => r.RoleTitle).IsRequired().HasMaxLength(20);
        }
    }

}
