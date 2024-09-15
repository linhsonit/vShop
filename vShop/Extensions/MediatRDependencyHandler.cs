using ProductService.Applications.Behaviours;

namespace ProductService.Extensions
{
    public static class MediatRDependencyHandler
    {
        public static IServiceCollection RegisterRequestHandlers(this IServiceCollection services)
        {
            return services.AddMediatR(cf =>
                    {
                        cf.RegisterServicesFromAssembly(typeof(Program).Assembly);

                        cf.AddOpenBehavior(typeof(LoggingBehaviour<,>));
                        cf.AddOpenBehavior(typeof(TransactionBehaviour<,>));
                    }
            );
        }
    }
}
