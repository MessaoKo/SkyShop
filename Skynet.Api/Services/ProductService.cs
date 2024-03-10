using AutoMapper;
using Skynet.Core.Contracts;
using Skynet.Core.Entities;
using Skynet.Core.Entities.Dto;

namespace Skynet.Api.Services;

public class ProductService : IProductService
{
	private readonly IProductRepository _repo;

	public ProductService(IProductRepository repo)
	{
		_repo = repo;
	}
	public async Task<IReadOnlyList<Product>> GetAll()
	{
		var result = await _repo.GetProductsAsync();

		if (result is null)
			throw new ArgumentException("Ce produit n'existe pas.", nameof(result));

		return result;
	}

	public async Task<Product> GetById(int id)
	{
		Product? product = await _repo.GetProductByIdAsync(id);

		if (product is null)
			throw new ArgumentException("Ce produit n'existe pas.", nameof(product));

		return product;
	}
	public async Task<Product> CreateAsync(Product product)
	{
		// Validation des données
		if (product is null)
			throw new ArgumentException("Données de produit invalides.", nameof(product));

		await _repo.AddProductAsync(product);

		// Map product to DTO
		return product;
	}

	
}
