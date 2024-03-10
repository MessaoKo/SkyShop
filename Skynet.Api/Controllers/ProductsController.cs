using Microsoft.AspNetCore.Mvc;
using Skynet.Core.Contracts;
using Skynet.Core.Entities;

namespace Skynet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
	private readonly ILogger<ProductsController> _logger;
	private readonly IProductService _productService;

	public ProductsController(ILogger<ProductsController> logger, IProductService productService)
	{
		_logger = logger;
		_productService = productService;
	}

	[HttpGet]
	public async Task<IActionResult> GetProducts()
	{
		try
		{
			var svcOut = await _productService.GetAll();
			if (svcOut is null)
				return NotFound();

			return Ok(svcOut);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet]
	[Route("api/{id:int}")]
	public async Task<IActionResult> GetProductById(int id)
	{
		try
		{
			Product? product = await _productService.GetById(id);

			if (product is null)
				return NotFound();

			return Ok(product);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromBody] Product product)
	{
		try
		{
			Product newProduct = await _productService.CreateAsync(product);
			return CreatedAtAction(nameof(GetProductById), new { Id = newProduct.Id }, newProduct);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}
