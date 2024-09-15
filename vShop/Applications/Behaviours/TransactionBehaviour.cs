using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.EventIntegrations.Events;

namespace ProductService.Applications.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ProductDbContext _dbContext;
        private readonly IProductIntegrationService _customerIntegrationService;

        public TransactionBehaviour(
            ProductDbContext dbContext,
            IProductIntegrationService customerIntegrationService)
        {
            _dbContext = dbContext;
            _customerIntegrationService = customerIntegrationService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = default(TResponse);
            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    await using var transaction = await _dbContext.BeginTransactionAsync();

                    response = await next();

                    await _dbContext.CommitTransactionAsync(transaction);

                    transactionId = transaction.TransactionId;


                    await _customerIntegrationService.PublishEventsThroughEventBusAsync(transactionId);
                });

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
