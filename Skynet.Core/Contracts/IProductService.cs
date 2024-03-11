using Skynet.Core.Entities;
namespace Skynet.Core.Contracts;

public interface IProductService
{
	Task<IReadOnlyList<Product?>> GetAll();
	Task<Product?> GetById(int id);
}