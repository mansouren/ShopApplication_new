using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Models
{
    public class ShoppingCartDto
    {
        [Display(Name = "تعداد")]
        public int ProductCount { get; set; }
    }
}
