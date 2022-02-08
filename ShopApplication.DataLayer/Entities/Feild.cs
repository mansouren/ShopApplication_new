using ShopApplication.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopApplication.DataLayer.Entities
{
    public class Feild : BaseEntity
    {
        [Display(Name = "نام مشخصه")]
        [Required(ErrorMessage = "لطفا مقداری وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string Name { get; set; }

        #region Relations
        public virtual ICollection<ProductFeild> ProductFeilds { get; set; }
        #endregion
    }


}
