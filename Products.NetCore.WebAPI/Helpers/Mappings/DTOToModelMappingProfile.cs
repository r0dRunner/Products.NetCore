using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Products.NetCore.Model;
using Products.NetCore.WebAPI.DTOs;

namespace Products.NetCore.WebAPI.Helpers.Mappings
{
    public class DTOToModelMappingProfile : Profile
    {
        public DTOToModelMappingProfile()
        {
            CreateMap<ProductDTO, ProductModel>();
            CreateMap<ProductOptionDTO, ProductOptionModel>();
        }
    }
}
