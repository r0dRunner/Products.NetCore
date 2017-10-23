using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Products.NetCore.Entity;
using Products.NetCore.Model;

namespace Products.NetCore.Service.Helpers.Mappings
{
    public class ModelToEntityMappingProfile : Profile
    {
        public ModelToEntityMappingProfile()
        {
            CreateMap<ProductModel, ProductEntity>();
            CreateMap<ProductOptionModel, ProductOptionEntity>();
        }
    }
}
