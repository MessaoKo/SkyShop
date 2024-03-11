using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skynet.Api.Errors;
using Skynet.Api.Services;
using Skynet.Core.Contracts;
using Skynet.Infrastructure.Data;

namespace Skynet.Api.Extensions;

public static class ApplicationServicesExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
	{
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		// AppContext Services
		services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlServer(config.GetConnectionString("Default"));
		});

		// EntityService
		services.AddScoped<IProductService, ProductService>();

		// Repository Services
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

		// Mapper Services
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		// Configuraton of Model Validation Error Handling
		services.Configure<ApiBehaviorOptions>(options =>
		{
			options.InvalidModelStateResponseFactory = actionContext =>
			{
				var errors = actionContext.ModelState
				.Where(e => e.Value!.Errors.Count > 0)
				.SelectMany(x => x.Value!.Errors)
				.Select(x => x.ErrorMessage).ToArray();

				var errorResponse = new ApiValidationErrorResponse
				{
					Errors = errors
				};

				return new BadRequestObjectResult(errorResponse);
			};
		});
		return services;
	}
}
