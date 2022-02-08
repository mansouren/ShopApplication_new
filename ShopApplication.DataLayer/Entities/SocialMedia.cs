using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApplication.DataLayer.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApplication.DataLayer.Entities
{
   public class SocialMedia : BaseEntity
    {
        [Display(Name = "شبکه اجتماعی")]
        public string SocialName { get; set; }

        [Display(Name = "آیکون")]
        public string SocialIcon { get; set; }
        
        [NotMapped]
        public IFormFile SocialIconFile { get; set; }

        [Display(Name = "لینک")]
        public string SocialLink { get; set; }

        [Display(Name = "عدم نمایش/نمایش")]
        public bool NotShow { get; set; }

        [Display(Name = "ترتیب نمایش")]
        public int SocialOrder { get; set; }
    }

    public class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
    {
        public void Configure(EntityTypeBuilder<SocialMedia> builder)
        {
            builder.Property(x => x.SocialName).IsRequired().HasMaxLength(100);
            //builder.Property(x => x.SocialIcon).IsRequired().HasMaxLength(150);
            builder.Property(x => x.SocialLink).IsRequired().HasMaxLength(200);
            
        }
    }

    public class SocialMediaValidation : AbstractValidator<SocialMedia>
    {
        public SocialMediaValidation()
        {
            RuleFor(x => x.SocialName).NotEmpty().WithMessage("لطفا مقداری وارد کنید")
                .MaximumLength(100).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
            //RuleFor(x => x.SocialIconFile).NotEmpty().WithMessage("لطفا یک تصویر انتخاب کنید");
               
            RuleFor(x => x.SocialLink).NotEmpty().WithMessage("لطفا مقداری وارد کنید")
               .MaximumLength(200).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
        }
    }
}
