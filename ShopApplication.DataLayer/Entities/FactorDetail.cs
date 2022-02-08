using ShopApplication.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopApplication.DataLayer.Entities
{
   public class FactorDetail : BaseEntity
    {
        public int FactorId { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
        public int UnitPrice { get; set; }
        
        #region Relations
        [ForeignKey(nameof(FactorId))]
        public virtual Factor Factor { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        #endregion
    }
}
