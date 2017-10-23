using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Products.NetCore.Model;
using Products.NetCore.WebAPI.DTOs;

namespace Products.NetCore.WebAPI.Helpers.Mappings
{
    public class ModelToDTOMappingProfile : Profile
    {
        public ModelToDTOMappingProfile()
        {
            CreateMap<ProductModel, ProductDTO>();
            CreateMap<ProductOptionModel, ProductOptionDTO>();
            CreateMap<IEnumerable<ProductModel>, CollectionDTO<ProductDTO>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));
            CreateMap<IEnumerable<ProductOptionModel>, CollectionDTO<ProductOptionDTO>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));
        }
    }
}
