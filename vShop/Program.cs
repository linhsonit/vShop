using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Data.Repositories;
using ProductService.EventIntegrations.EventHandling;
using ProductService.EventIntegrations.Events;
using ProductService.Extensions;
using ProductService.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.RegisterRequestHandlers();

builder.Services.AddDbContext<ProductDbContext>(options => 
                                options.UseSqlServer(builder.Configuration.GetConnectionString("ProductServiceConnection")));

builder.Services.AddScoped<IProductIntegrationService, ProductIntegrationService>();

builder.Services.AddSubscription<ProductCreateSucceededIntegrationEvent, ProductCreateSucceededIntegrationEventHandler>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
