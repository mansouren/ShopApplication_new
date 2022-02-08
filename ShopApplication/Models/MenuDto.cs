using FluentValidation;
using ShopApplication.DataLayer.Entities;
using ShopApplication.WebFrameWorks.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApplication.Models
{
    public class MenuDto : BaseDTO<MenuDto,Menu>
    {
        [Display(Name = "نام منو")]
        public string Name { get; set; }

        [Display(Name = "عدم نمایش")]
        public bool NotShow { get; set; }

        [Display(Name = "ترتیب نمایش")]
        public int Order { get; set; }

        [Display(Name = "محتویات")]
        //[DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
    }

    public class MenuDtoValidation : AbstractValidator<MenuDto>
    {
        public MenuDtoValidation()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("لطفا مقداری وارد کنید.")
                .MaximumLength(30).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
            
        }
    }
}
