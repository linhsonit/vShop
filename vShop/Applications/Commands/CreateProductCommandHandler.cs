using MediatR;
using ProductService.Data;
using ProductService.Data.Repositories;
using ProductService.EventIntegrations.EventBus.Abstractions;
using ProductService.EventIntegrations.Events;
using ProductService.IntegrationEvents.Events;

namespace ProductService.Applications.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductDbContext _productDbContext;
        private readonly IProductIntegrationService _productIntegrationService;
        private readonly IServiceProvider _serviceProvider;

        public CreateProductCommandHandler(
            IProductRepository productRepository, 
            ProductDbContext productDbContext,
            IProductIntegrationService productIntegrationService, 
            IServiceProvider serviceProvider)
        {
            _productRepository = productRepository;
            _productDbContext = productDbContext;
            _productIntegrationService = productIntegrationService;
            _serviceProvider = serviceProvider;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productCreateIntegartionEvent = new ProductCreateIntegrationEvent(request.Product);
            await _productIntegrationService.AddAndSaveEventAsync(productCreateIntegartionEvent, _productDbContext.GetCurrentTransaction());

            // If command... do some add insert.

            await using var scope = _serviceProvider.CreateAsyncScope();


            // Ví dụ handle message nhận được tại RabbitMQ.
            var handler = scope.ServiceProvider.GetKeyedServices<IIntegrationEventHandler>(typeof(ProductCreateSucceededIntegrationEvent));
            await handler.First().Handle(productCreateIntegartionEvent);

            return await _productRepository.CreateProductAsync(request.Product);
        }
    }
}
