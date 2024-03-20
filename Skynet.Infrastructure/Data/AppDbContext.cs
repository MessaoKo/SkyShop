using Skynet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Skynet.Infrastructure.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}

	public DbSet<Product> Products { get; set; }

	public DbSet<ProductType> ProductTypes { get; set; }

	public DbSet<ProductBrand> ProductBrands { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
