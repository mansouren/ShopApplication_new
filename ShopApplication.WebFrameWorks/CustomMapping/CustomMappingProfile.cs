using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.WebFrameWorks.CustomMapping
{
   public class CustomMappingProfile : Profile
    {
        public CustomMappingProfile(IEnumerable<IHaveCustomMapping> customMappings)
        {
            foreach (var mapping in customMappings)
            {
                mapping.CreateMapping(this);
            }
        }
    }
}
