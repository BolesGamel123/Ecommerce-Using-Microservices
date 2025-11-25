using Asp.Versioning;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Repositories;
using Catolog.Infrastructure.Data.Contexts;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(GetProductByIdQuery).Assembly);
});

builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, ProductRepository>();
builder.Services.AddScoped<ITypeRepository, ProductRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);

});


builder.Services.AddSwaggerGen(options =>
{
    // Basic info
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catolog API",
        Version = "v1",
        Description = "this is Api for Catolog Ecommerce Application",
        Contact = new OpenApiContact
        {
            Name = "Boles Gamel",
            Email = "boles.gamel123@gmail.com"
        }

    });
});
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ICatalogContext>();

    await BrandContextSeed.SeedDataAsync(context.Brands);
    await TypeContextSeed.SeedDataAsync(context.Types);
    await CatalogContextSeed.SeedDataAsync(context.Products);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
