using AutoMapper;
using ShopApplication.DataLayer.Entities.Common;
using ShopApplication.WebFrameWorks.CustomMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.WebFrameWorks.Core
{
   public abstract class BaseDTO<TDto,TEntity,TKey> : IHaveCustomMapping
        where TDto : class,new()
        where TEntity:BaseEntity<TKey>,new()
    {
        public TKey Id { get; set; }

        public TEntity ToEntity(IMapper mapper)
        {
            return mapper.Map<TEntity>(CastToDrivedClass(mapper, this));
        }
        public TEntity ToEntity(IMapper mapper,TEntity entity)
        {
           return mapper.Map(CastToDrivedClass(mapper,this), entity);
        }

        public static TDto FromEntity(IMapper mapper,TEntity model)
        {
           return mapper.Map<TDto>(model);
        }

        protected TDto CastToDrivedClass(IMapper mapper, BaseDTO<TDto, TEntity, TKey> baseInstance)
        {
            return mapper.Map<TDto>(baseInstance);
        }
        public void CreateMapping(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TDto, TEntity>();
            var entityType = typeof(TEntity);
            var dtoType = typeof(TDto);
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            CustomMapping(mappingExpression.ReverseMap());
        }

        public virtual void CustomMapping(IMappingExpression<TEntity,TDto> mapping)
        {

        }
    }

    public abstract class BaseDTO<TDto,TEntity> : BaseDTO<TDto,TEntity,int>
        where TDto : class,new()
        where TEntity:BaseEntity<int>,new()
    {

    }
}
