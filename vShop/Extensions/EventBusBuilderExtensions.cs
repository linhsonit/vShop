using ProductService.EventIntegrations.EventBus;
using ProductService.EventIntegrations.EventBus.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace ProductService.Extensions
{
    public static class EventBusBuilderExtensions
    {
        public static IServiceCollection AddSubscription<T, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TH>
            (this IServiceCollection services) 
            where T : IntegrationEvent
            where TH : class, IIntegrationEventHandler<T>
        {
            services.AddKeyedTransient<IIntegrationEventHandler, TH>(typeof(T));

            //services.Configure<EventBusSubscriptionInfo>(o =>
            //{
            //    // Keep track of all registered event types and their name mapping. We send these event types over the message bus
            //    // and we don't want to do Type.GetType, so we keep track of the name mapping here.

            //    // This list will also be used to subscribe to events from the underlying message broker implementation.
            //    o.EventTypes[typeof(T).Name] = typeof(T);
            //});

            return services;
        }
    }
}
