using Skynet.Core.Entities;
using Skynet.Core.Specifications;

namespace Skynet.Core.Contracts;

public interface IGenericRepository<T> where T : BaseEntity
{
	Task<IReadOnlyList<T>> ListAllAsync();
	Task<T?> GetByIdAsync(int id);
	Task<T?> GetEntityWithSpec(ISpecification<T> spec);
	Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
}
