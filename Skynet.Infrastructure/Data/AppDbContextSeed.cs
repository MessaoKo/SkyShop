using Microsoft.EntityFrameworkCore;
using Skynet.Core.Entities;
using System.Text.Json;

namespace Skynet.Infrastructure.Data;
public class AppDbContextSeed
{
	public static async Task SeedAsync(AppDbContext context)
	{
		if (!context.ProductBrands.Any())
		{
			string jsonPath = "C:\\Users\\kouum\\Desktop\\SkyShop\\Skynet.Infrastructure\\Data\\SeedData\\brands.json";

			
			string brandsData = File.ReadAllText(jsonPath);
			var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData) ?? throw new Exception("Invalid source data Json");
			await context.ProductBrands.AddRangeAsync(brands);
		}
		if (!context.ProductTypes.Any())
		{
			string jsonPath = "C:\\Users\\kouum\\Desktop\\SkyShop\\Skynet.Infrastructure\\Data\\SeedData\\types.json";
			string typesData = File.ReadAllText(jsonPath);
			var types = JsonSerializer.Deserialize<List<ProductType>>(typesData) ?? throw new Exception("Invalid source data Json");
			await context.ProductTypes.AddRangeAsync(types);
		}

		if (!context.Products.Any())
		{
			string jsonPath = "C:\\Users\\kouum\\Desktop\\SkyShop\\Skynet.Infrastructure\\Data\\SeedData\\products.json";
			string productsData = File.ReadAllText(jsonPath);
			var products = JsonSerializer.Deserialize<List<Product>>(productsData) ?? throw new Exception("Invalid source data Json");
			await context.Products.AddRangeAsync(products);
		}

		if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
	}
}
