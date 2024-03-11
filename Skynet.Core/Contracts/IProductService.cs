using Skynet.Core.Dtos;
using Skynet.Core.Entities;
namespace Skynet.Core.Contracts;

public interface IProductService
{
	Task<IReadOnlyList<ProductToReturnDto?>> GetAll();
	Task<ProductToReturnDto?> GetById(int id);
}