using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Applications.Commands;
using ProductService.Applications.Requests;
using ProductService.Data.Model;

namespace ProductService.Apis
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ProductController> _logger;

        private readonly IMediator _mediator;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("productId")]
        public async Task<Product?> GetCustomerAsync(int productId)
        {
            var product = await _mediator.Send(new GetProductRequest() { ProductId = productId });

            return product;
        }

        [HttpPost]
        public async Task<int> CreateCustomerAsync(Product product)
        {
            var productId = await _mediator.Send(new CreateProductCommand() { Product = product });
            return productId;
        }

    }
}
