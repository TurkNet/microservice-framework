using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Noctools.Application.CleanArch
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// TxRequestHandlerBase must be used in use cases
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCleanArch(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            return services;
        }
    }
}
