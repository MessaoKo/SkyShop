using Skynet.Core.Dtos;
using Skynet.Core.Entities;
using Skynet.Core.Specifications;
namespace Skynet.Core.Contracts;

public interface IProductService
{
	Task<IReadOnlyList<ProductToReturnDto?>> GetAll(ProductSpecParams productParams);
	Task<ProductToReturnDto?> GetById(int id);
}