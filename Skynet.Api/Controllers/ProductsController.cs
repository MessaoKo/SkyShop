using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skynet.Core.Contracts;
using Skynet.Core.Dtos;
using Skynet.Core.Entities;
using Skynet.Infrastructure.Data;

namespace Skynet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
	private readonly ILogger<ProductsController> _logger;
	private readonly IProductService _productService;
	private readonly AppDbContext _appDbContext;


	public ProductsController(ILogger<ProductsController> logger, IProductService productService, AppDbContext appDbContext)
	{
		_logger = logger;
		_productService = productService;
		_appDbContext = appDbContext;
	}

	[HttpGet]
	public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
	{
		try
		{
			var svcOut = await _productService.GetAll() ?? throw new ArgumentNullException();
			return Ok(svcOut);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("types")]

	public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
	{
		return Ok(await _appDbContext.ProductTypes.ToListAsync());
	}


	[HttpGet("brands")]
	public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductBrands()
	{
		return Ok(await _appDbContext.ProductBrands.ToListAsync());
	}

	[HttpGet]
	[Route("{id:int}")]
	public async Task<IActionResult> GetProductById(int id)
	{
		try
		{
			ProductToReturnDto? product = await _productService.GetById(id);

			if (product is null)
				return NotFound();

			return Ok(product);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}
