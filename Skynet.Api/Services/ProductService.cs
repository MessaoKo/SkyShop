using Skynet.Core.Contracts;
using Skynet.Core.Entities;
using Skynet.Core.Specifications;

namespace Skynet.Api.Services;

public class ProductService : IProductService
{
	private readonly IGenericRepository<Product> _productRepo;

	public ProductService(IGenericRepository<Product> productRepo)
	{
		_productRepo = productRepo;
	}

	public async Task<IReadOnlyList<Product?>> GetAll()
	{
		var spec = new ProductsWithTypesAndBrandsSpecification();

		var result = await _productRepo.ListAsync(spec);

		return result;
	}

	public async Task<Product?> GetById(int id)	
	{
		var spec = new ProductsWithTypesAndBrandsSpecification(id);

		return await _productRepo.GetEntityWithSpec(spec);
	}
}
