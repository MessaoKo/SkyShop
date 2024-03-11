using AutoMapper;
using Skynet.Core.Dtos;
using Skynet.Core.Entities;

namespace Skynet.Api.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(destination => destination.Brand, options => options.MapFrom(src => src.ProductBrand.Name))
            .ForMember(destination => destination.Type, options => options.MapFrom(src => src.ProductType.Name))
            .ForMember(destination => destination.PictureUrl, options => options.MapFrom<ProductUrlResolver>());
    }
}
