using Skynet.Core.Contracts;
using Skynet.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Skynet.Infrastructure.Data;

public class ProductRepository : IProductRepository
{	
	private readonly AppDbContext _context;

	public ProductRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task AddProductAsync(Product product)
	{	
		await _context.Products.AddAsync(product);
		await _context.SaveChangesAsync();
	}
	 
	public async Task<Product?> GetProductByIdAsync(int id)
	{
		return await _context.Products
			.Include(p => p.ProductBrand)
			.Include(p => p.ProductType)
			.FirstOrDefaultAsync(p => p.Id == id);
	}

	public async Task<IReadOnlyList<Product>> GetProductsAsync()
	{
		return await _context.Products
			.Include(p => p.ProductBrand)
			.Include(p => p.ProductType)
			.ToListAsync();
	}
}
