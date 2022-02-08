using FluentValidation;
using ShopApplication.DataLayer.Entities;
using ShopApplication.WebFrameWorks.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Models
{
    public class RegisterDto : BaseDTO<RegisterDto, User>
    {

        public string Mobile { get; set; }

        public string Password { get; set; }

        public string RePassword { get; set; }

       

        public class RegisterValidation : AbstractValidator<RegisterDto>
        {
            public RegisterValidation()
            {
                RuleFor(u => u.Mobile).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                   .MaximumLength(11).WithMessage("شماره موبایل نمی تواند بیشتر از 11 کاراکتر باشد")
                   .MinimumLength(11).WithMessage("{PropertyName}نمی تواند کمتر از {0} کاراکتر باشد");


                RuleFor(u => u.Password).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                    .MaximumLength(50).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");

                RuleFor(u => u.RePassword).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                    .DependentRules(() =>
                    {
                        RuleFor(u => u.RePassword).Must(Repassword => Repassword != "{Password}")
                        .WithMessage("کلمه های عبور وارد شده با یکدیگر همخوانی ندارند");
                    });



            }
        }

    }

    public class LoginDto : BaseDTO<LoginDto,User>
    {
        public string Mobile { get; set; }

        public string Password { get; set; }
    }

    public class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(u => u.Mobile).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                   .MaximumLength(11).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");

            RuleFor(u => u.Password).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                .MaximumLength(50).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");
        }
    }

    public class ActivateDto : BaseDTO<ActivateDto, User>
    {
        public string UserCode { get; set; }
    }

    public class ActivateValidation : AbstractValidator<ActivateDto>
    {
        public ActivateValidation()
        {
            RuleFor(a => a.UserCode).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                .MaximumLength(6).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");
        }
    }

    public class CheckMobileDto : BaseDTO<CheckMobileDto,User>
    {
        public string Mobile { get; set; }
    }

    public class CheckMobileDtoValidation : AbstractValidator<CheckMobileDto>
    {
        public CheckMobileDtoValidation()
        {
            RuleFor(c => c.Mobile).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                .MaximumLength(11).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");
        }
    }

    public class ForgetPasswordDto : BaseDTO<ForgetPasswordDto,User>
    {
        [Display(Name ="کد تائید")]
        public string UserCode { get; set; }
        
        public string Password { get; set; }

        public string RePassword { get; set; }
    }

    public class ForgetPasswordDtoValidation : AbstractValidator<ForgetPasswordDto>
    {
        public ForgetPasswordDtoValidation()
        {
            RuleFor(a => a.UserCode).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
              .MaximumLength(6).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");

            RuleFor(u => u.Password).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                .MaximumLength(50).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");

            RuleFor(u => u.RePassword).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                .DependentRules(() =>
                {
                    RuleFor(u => u.RePassword).Must(Repassword => Repassword != "{Password}")
                    .WithMessage("کلمه های عبور وارد شده با یکدیگر همخوانی ندارند");
                });

        } 
    }

        public class ChangePasswordDto : BaseDTO<ChangePasswordDto, User>
        {
            public string OldPassword { get; set; }

            public string Password { get; set; }

            public string RePassword { get; set; }
        }

        public class ChangePasswordDtoConfiguration : AbstractValidator<ChangePasswordDto>
        {
            public ChangePasswordDtoConfiguration()
            {
                RuleFor(x => x.OldPassword).NotEmpty().WithMessage("لطفا مقداری وارد کنید")
                       .MaximumLength(50).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");
                RuleFor(x => x.Password).NotEmpty().WithMessage("لطفا مقداری وارد کنید")
                           .MaximumLength(50).WithMessage("{PropertyName} نمی تواند بیشتر از {0} کاراکتر باشد");
                RuleFor(x => x.RePassword).NotEmpty().WithMessage("لطفا مقداری وارد کنید")
                       .DependentRules(() =>
                       {
                           RuleFor(x => x.RePassword).Must(RePassword => RePassword != "{Password}")
                                  .WithMessage("کلمه های عبور وارد شده با یکدیگر همخوانی ندارند");
                       });
            }
        }
    }

