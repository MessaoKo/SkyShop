using System.Linq.Expressions;

namespace Skynet.Core.Specifications;

public interface ISpecification<T>
{
	Expression<Func<T,bool>> Criteria { get; }

	List<Expression<Func<T,object>>> Includes { get;}
}
