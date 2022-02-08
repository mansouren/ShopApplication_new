using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApplication.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopApplication.DataLayer.Entities
{
   public class Settings : BaseEntity
    { 
        [Display(Name = "نام فروشگاه")]
        public string Name { get; set; }

        [Display(Name = "توضیح مختصر")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [DataType(DataType.MultilineText)]
        public string KeyWords { get; set; }

        [Display(Name = "تلفن")]
        public string Telephone { get; set; }

        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "فکس")]
        public string Fax { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        //[Display(Name = "نام کاربری سرویس sms")]
        public string SmsServiceUserName { get; set; }

        //[Display(Name = "رمز عبور")]
        public string SmsServicePassword { get; set; }

        //[Display(Name = "شماره فرستنده")]
        public string SmsServiceNumber { get; set; }

        //[Display(Name = "ارسال پیامک پس از صدور فاکتور")]
        public bool FactorIsSend { get; set; }

        //[Display(Name = "ارسال پیامک پس از پرداخت")]
        public bool PayIsSend { get; set; }

    }

    public class SettingConfiguration : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.Property(s => s.Name).HasMaxLength(100);
            builder.Property(s => s.Telephone).HasMaxLength(20);
            builder.Property(s => s.Mobile).HasMaxLength(11);
            builder.Property(s => s.Fax).HasMaxLength(20);
            builder.Property(s => s.Address).HasMaxLength(200);
            builder.Property(s => s.SmsServiceUserName).HasMaxLength(20);
            builder.Property(s => s.SmsServicePassword).HasMaxLength(20);
            builder.Property(s => s.SmsServiceNumber).HasMaxLength(15);


        }
    }
}
