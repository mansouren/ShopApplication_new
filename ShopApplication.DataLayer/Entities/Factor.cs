using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApplication.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopApplication.DataLayer.Entities
{
   public class Factor : BaseEntity
    {
        public int UserId { get; set; }
        public int? AddressId { get; set; }
        public string Number { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? PayDate { get; set; }
        public DateTime? PayTime { get; set; }
        public string PayNumber { get; set; }
        public bool IsPayed { get; set; }
        public int Price { get; set; }

        #region Relations
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual ICollection<FactorDetail> FactorDetails { get; set; }

        #endregion

    }

    public class FactorConfiguration : IEntityTypeConfiguration<Factor>
    {
        public void Configure(EntityTypeBuilder<Factor> builder)
        {
            builder.Property(f => f.Number).IsRequired().HasMaxLength(6);
            builder.Property(f => f.PayNumber).HasMaxLength(20);
            builder.Property(f => f.CreateDate).IsRequired();
        }
    }
}
