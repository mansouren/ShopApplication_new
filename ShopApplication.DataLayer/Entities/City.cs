using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApplication.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopApplication.DataLayer.Entities
{
   public class City : BaseEntity
    {
        [Display(Name ="نام شهر")]
        public string Name { get; set; }
        public int StateId { get; set; }

        #region Relations
        [ForeignKey(nameof(StateId))]
        public State State { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        #endregion

    }

    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        }
    }
}
