using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApplication.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DataLayer.Entities
{
   public class Group : BaseEntity
    {
        public string Name { get; set; }
        public bool NotShow { get; set; }
        public int Order { get; set; }

        #region Relations
        public virtual ICollection<Product> Products { get; set; }
        #endregion

    }

    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(g => g.Name).IsRequired().HasMaxLength(30);
        }
    }
}
