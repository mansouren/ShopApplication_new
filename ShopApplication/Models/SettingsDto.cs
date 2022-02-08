using FluentValidation;
using Microsoft.AspNetCore.Http;
using ShopApplication.DataLayer.Entities;
using ShopApplication.WebFrameWorks.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Models
{
    public class SettingsDto : BaseDTO<SettingsDto, Settings>
    {
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string KeyWords { get; set; }
    }

    public class SettingsValidation : AbstractValidator<SettingsDto>
    {
        public SettingsValidation()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("لطفا مقداری وارد کنید")
                .MaximumLength(100).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");

        }
    }

    public class CallSettingsDto : BaseDTO<CallSettingsDto, Settings>
    {
        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Fax { get; set; }

        public string Address { get; set; }
    }

    public class CallSettingsValidation : AbstractValidator<CallSettingsDto>
    {
        public CallSettingsValidation()
        {
            RuleFor(x => x.Mobile).MaximumLength(11).WithMessage("لطفا شماره همراه معتبر وارد کنید")
                                  .MinimumLength(11).WithMessage("لطفا شماره همراه معتبر وارد کنید");

            RuleFor(x => x.Fax).MaximumLength(20).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
            RuleFor(x => x.Telephone).MaximumLength(20).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
            RuleFor(x => x.Address).MaximumLength(200).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");

        }
    }

    public class SmsSettingsDto : BaseDTO<SmsSettingsDto, Settings>
    {
        [Display(Name = "نام کاربری سرویس sms")]
        public string SmsServiceUserName { get; set; }

        [Display(Name = "رمز عبور")]
        public string SmsServicePassword { get; set; }

        [Display(Name = "شماره فرستنده")]
        public string SmsServiceNumber { get; set; }

        [Display(Name = "ارسال پیامک پس از صدور فاکتور")]
        public bool FactorIsSend { get; set; }

        [Display(Name = "ارسال پیامک پس از پرداخت")]
        public bool PayIsSend { get; set; }
    }

    public class SmsSettingsValidation : AbstractValidator<SmsSettingsDto>
    {
        public SmsSettingsValidation()
        {
            RuleFor(x => x.SmsServiceNumber).MaximumLength(15)
                          .WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");

            RuleFor(x => x.SmsServiceUserName).MaximumLength(20)
                .WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");

            RuleFor(x => x.SmsServicePassword).MaximumLength(20)
                .WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
        }
    }

    public class SliderDto : BaseDTO<SliderDto, Slider>
    {
        [Display(Name ="عنوان اسلاید")]
        public string Title { get; set; }

        [Display(Name = "انتخاب تصویر")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "انتخاب تصویر")]
        public string Imgname { get; set; }

        [Display(Name = "ترتیب نمایش")]
        public int Order { get; set; }

        [Display(Name = "عدم نمایش")]
        public bool NotShow { get; set; }
    }

    public class SliderDtoValidation : AbstractValidator<SliderDto>
    {
        public SliderDtoValidation()
        {
            //RuleFor(s => s.Imgname).MaximumLength(100).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
            RuleFor(s => s.Title).NotEmpty().WithMessage("لطفا مقداری وارد کنید").MaximumLength(100)
                                 .WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
            //RuleFor(s => s.ImageFile).NotEmpty().WithMessage("لطفا مقداری وارد کنید");
        }
    }

    public class BrandDto : BaseDTO<BrandDto, Brand>
    {
        [Display(Name ="عنوان برند")]
        public string Title { get; set; }

        [Display(Name = "عدم نمایش")]
        public bool NotShow { get; set; }

        [Display(Name = "ترتیب نمایش")]
        public int Order { get; set; }

        [Display(Name = "انتخاب تصویر")]
        public string ImageName { get; set; }

        public IFormFile ImageFile { get; set; }
    }

    public class BrandDtoValidation : AbstractValidator<BrandDto>
    {
        public BrandDtoValidation()
        {
            RuleFor(b => b.ImageName).MaximumLength(100).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
            RuleFor(b => b.Title).NotEmpty().WithMessage("لطفا مقداری وارد کنید")
                .MaximumLength(30).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
        }
    }

    public class GroupDto : BaseDTO<GroupDto, Group>
    {
        public string Name { get; set; }
        public bool NotShow { get; set; }
        public int Order { get; set; }
    }

    public class GroupDtoValidation : AbstractValidator<GroupDto>
    {
        public GroupDtoValidation()
        {
            RuleFor(g => g.Name).NotEmpty().WithMessage("لطفا مقداری وارد کنید")
                .MaximumLength(30).WithMessage("{PropertyName}نباید بیشتر از {0} کاراکتر باشد");
            
        }
    }
}

