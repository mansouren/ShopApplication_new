using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DataLayer.Entities.Common
{
    public interface IEntity
    {

    }
   public abstract class BaseEntity<Tkey> : IEntity
    {
        public Tkey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {

    }
}
