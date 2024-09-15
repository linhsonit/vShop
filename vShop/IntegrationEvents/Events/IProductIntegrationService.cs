using Microsoft.EntityFrameworkCore.Storage;
using ProductService.EventIntegrations.EventBus;

namespace ProductService.EventIntegrations.Events
{
    public interface IProductIntegrationService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt, IDbContextTransaction transaction);
    }
}
