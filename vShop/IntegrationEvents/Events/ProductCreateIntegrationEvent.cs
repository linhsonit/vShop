using ProductService.Data.Model;
using ProductService.EventIntegrations.EventBus;

namespace ProductService.EventIntegrations.Events
{
    public class ProductCreateIntegrationEvent : IntegrationEvent
    {
        public Product Product { get; set; }

        public ProductCreateIntegrationEvent(Product product)
                => Product = product;
    }
}
