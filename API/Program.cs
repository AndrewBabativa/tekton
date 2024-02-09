using Core.Commands;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.ExternalServices;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.AspNetCore.Hosting;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(CreateProductCommandHandler).Assembly);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDiscountService, ExternalDiscountService>();

builder.Services.AddDbContext<ProductsContext>(options =>
{
    options.UseInMemoryDatabase("Products");
});


builder.Services.AddDbContext<ProductsContext>();
builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);


builder.Services.AddMemoryCache();
builder.Services.AddScoped<ProductStatusCache>();
builder.Services.AddHttpClient<ExternalDiscountService>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Technical test .NET Andres Babativa Goyeneche");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<TimingMiddleware>();

app.Run();
