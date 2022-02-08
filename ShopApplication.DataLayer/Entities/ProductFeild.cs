using ShopApplication.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopApplication.DataLayer.Entities
{
    public class ProductFeild : BaseEntity
    {
        public int ProductId { get; set; }
        public int FeildId { get; set; }

        [Display(Name = "مقدار مشخصه")]
        [Required(ErrorMessage = "لطفا مقداری وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string FeildValue { get; set; }

        #region Relations

        [ForeignKey(nameof(FeildId))]
        public virtual Feild Feild { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        #endregion
    }
}
