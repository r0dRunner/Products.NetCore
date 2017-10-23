using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Products.NetCore.Entity;
using Products.NetCore.Model;

namespace Products.NetCore.Service.Helpers.Mappings
{
    public class EntityToModelMappingProfile : Profile
    {
        public EntityToModelMappingProfile()
        {
            CreateMap<ProductEntity, ProductModel>();
            CreateMap<ProductOptionEntity, ProductOptionModel>();
        }
    }
}
