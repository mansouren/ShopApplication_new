using Microsoft.AspNetCore.Http;
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
    public class ProductDto : BaseDTO<ProductDto, Product>
    {
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage ="لطفا مقداری وارد کنید")]
        [MaxLength(100,ErrorMessage ="{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string Name { get; set; }

        [Display(Name = "انتخاب گروه")]
        [Required(ErrorMessage = "لطفا یک گروه انتخاب کنید")]
        public int GroupId { get; set; }

        [Display(Name = "انتخاب برند")]
        [Required(ErrorMessage = "لطفا یک برندانتخاب کنید")]
        public int BrandId { get; set; }

        [MaxLength(100,ErrorMessage ="{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string Img { get; set; }

        [Display(Name = "انتخاب تصویر")]
        public IFormFile ImgFile { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }
        
        [Display(Name = "موجودی")]
        public int Qty { get; set; }

        public bool NotShow { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int Seen { get; set; }


        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

     
    }
}
