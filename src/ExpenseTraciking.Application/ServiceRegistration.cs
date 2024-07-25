using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTraciking.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly);

                //config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
                //config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            //services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly, includeInternalTypes: true);


        }
    }
}
