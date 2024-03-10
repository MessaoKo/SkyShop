namespace Skynet.Core.Entities.Dto;

public record ProductDto
{
	public string Name { get; init; } = string.Empty;
	public string Description { get; init; } = string.Empty;
	public decimal Price { get; init; }
	public string PictureUrl { get; init; } = string.Empty;

	public ProductType ProductType { get; init; }
	public int ProductTypeId { get; init; }

	public ProductBrand ProductBrand { get; init; }
	public int ProductBrandId { get; init; }

	public DateTime CreatedAt { get; init; } = DateTime.Now;
	public DateTime? UpdatedAt { get; init; }
}