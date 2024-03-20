using Skynet.Core.Entities;
using System.Linq.Expressions;

namespace Skynet.Core.Specifications
{
	public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
	{
		public ProductsWithTypesAndBrandsSpecification()
		{
			AddInclude(x => x.ProductType);
			AddInclude(x => x.ProductBrand);
		}

		public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
		{
			AddInclude(x => x.ProductType);
			AddInclude(x => x.ProductBrand);
		}
	}
}
