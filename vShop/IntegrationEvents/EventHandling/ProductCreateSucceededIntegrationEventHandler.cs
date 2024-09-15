using ProductService.EventIntegrations.EventBus;
using ProductService.EventIntegrations.EventBus.Abstractions;
using ProductService.IntegrationEvents.Events;

namespace ProductService.EventIntegrations.EventHandling
{
    public class ProductCreateSucceededIntegrationEventHandler : IIntegrationEventHandler<ProductCreateSucceededIntegrationEvent>
    {
        public Task Handle(IntegrationEvent @event)
        {
            throw new NotImplementedException();
        }

    }
}
