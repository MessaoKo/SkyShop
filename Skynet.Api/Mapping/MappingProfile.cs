using AutoMapper;
using Skynet.Core.Entities;
using Skynet.Core.Entities.Dto;

namespace API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product,ProductDto>();
        CreateMap<ProductDto,Product>();
	}
}
