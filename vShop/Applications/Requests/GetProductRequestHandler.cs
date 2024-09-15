using MediatR;
using ProductService.Data;
using ProductService.Data.Model;
using ProductService.Data.Repositories;
using ProductService.EventIntegrations.Events;

namespace ProductService.Applications.Requests
{
    public class GetProductRequestHandler : IRequestHandler<GetProductRequest, Product?>
    {
        private readonly ProductDbContext _productDbContext;
        private readonly IProductRepository _productRepository;
        private readonly IProductIntegrationService _productIntegrationService;

        public GetProductRequestHandler(
            ProductDbContext productDbContext,
            IProductRepository productRepository,
            IProductIntegrationService productIntegrationService)
        {
            _productDbContext = productDbContext;
            _productRepository = productRepository;
            _productIntegrationService = productIntegrationService;
        }

        public async Task<Product?> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
          

            
            return await _productRepository.GetProductAsync(request.ProductId);
        }
    }
}
