using Skynet.Core.Contracts;
using Skynet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Skynet.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mapper Services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// AppContext Services
builder.Services.AddDbContext<AppDbContext>(
	options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Entities Services
builder.Services.AddScoped<IProductService, ProductService>();


// Repository Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Apply migrations and update database before app is run.
using var scope  = app.Services.CreateScope();

var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{   
    await context.Database.MigrateAsync();
    await AppDbContextSeed.SeedAsync(context);
}
catch(Exception ex)
{
    logger.LogError( ex, "An error occured during migration");
}

app.Run();
