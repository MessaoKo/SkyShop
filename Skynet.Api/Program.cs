using Skynet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Skynet.Api.Middleware;
using Skynet.Api.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DefaultPolicy");

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
