using Skynet.Core.Entities;

namespace Skynet.Core.Contracts;

public interface IProductRepository
{
	Task<Product?> GetProductByIdAsync(int id);
	Task<IReadOnlyList<Product>> GetProductsAsync();
	Task AddProductAsync(Product product);
}
