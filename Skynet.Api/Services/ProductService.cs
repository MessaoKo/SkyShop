using AutoMapper;
using Skynet.Core.Contracts;
using Skynet.Core.Dtos;
using Skynet.Core.Entities;
using Skynet.Core.Specifications;

namespace Skynet.Api.Services;

public class ProductService : IProductService
{
	private readonly IMapper _mapper;
	private readonly IGenericRepository<Product> _productRepo;
	

	public ProductService(IMapper mapper, IGenericRepository<Product> productRepo)
	{
		_mapper = mapper;
		_productRepo = productRepo;
	}

	public async Task<IReadOnlyList<ProductToReturnDto?>> GetAll()
	{
		var spec = new ProductsWithTypesAndBrandsSpecification();

		IReadOnlyList<Product> products = await _productRepo.ListAsync(spec);

		IReadOnlyList<ProductToReturnDto> productsDtos = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

		return productsDtos;
	}

	public async Task<ProductToReturnDto?> GetById(int id)	
	{
		var spec = new ProductsWithTypesAndBrandsSpecification(id);

		Product? product = await _productRepo.GetEntityWithSpec(spec);

		ProductToReturnDto? productDto = _mapper.Map<ProductToReturnDto>(product);

		return productDto;
	}
}
