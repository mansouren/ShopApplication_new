using FluentValidation;
using ShopApplication.DataLayer.Entities;
using ShopApplication.WebFrameWorks.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Models
{
    public class UserDto : BaseDTO<UserDto, User>
    {
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string UserCode { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
    }

    public class UserDtoValidation : AbstractValidator<UserDto>
    {
        public UserDtoValidation()
        {

            RuleFor(u => u.Mobile).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
               .MaximumLength(11).WithMessage("شماره موبایل نمی تواند بیشتر از 11 کاراکتر باشد")
               .MinimumLength(11).WithMessage("{PropertyName}نمی تواند کمتر از {0} کاراکتر باشد");
           
            RuleFor(u => u.RoleId).NotEmpty().WithMessage("لطفا یک نقش برای کاربر انتخاب کنید");

            RuleFor(u => u.Password).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                .MaximumLength(50).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");
           
            //RuleFor(a => a.UserCode).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
            //    .MaximumLength(6).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");
        }
    }
}
