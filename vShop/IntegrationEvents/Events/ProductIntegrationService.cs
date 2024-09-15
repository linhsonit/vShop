using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProductService.Data;
using ProductService.Data.Model;
using ProductService.EventIntegrations.EventBus;

namespace ProductService.EventIntegrations.Events
{
    public class ProductIntegrationService : IProductIntegrationService
    {
        private readonly ProductDbContext _dbContext;
        public ProductIntegrationService(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAndSaveEventAsync(IntegrationEvent evt, IDbContextTransaction transaction)
        {
            var eventLogEntry = new IntegrationEventLogEntry(evt, transaction.TransactionId);
            _dbContext.Set<IntegrationEventLogEntry>().Add(eventLogEntry);

            return _dbContext.SaveChangesAsync();
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _dbContext.Set<IntegrationEventLogEntry>()
                .Where(p => p.State == EventStateEnum.NotPublished).ToListAsync();

            foreach (var logEvt in pendingLogEvents)
            {
                // MarkEventAsInProgressAsync
                // PublishEvent
                // MarkEventAsPublishedAsync
            }
        }
    }
}
