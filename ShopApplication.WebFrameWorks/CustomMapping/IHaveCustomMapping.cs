using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.WebFrameWorks.CustomMapping
{
   public interface IHaveCustomMapping
    {
        void CreateMapping(Profile profile);
    }
}
