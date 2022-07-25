using Microsoft.Extensions.DependencyInjection;

namespace Noctools.Infrastructure.Features
{
    public static class FeatureExtensions
    {
        public static void AddFeatureToggle(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IFeature), typeof(Feature));
        }
    }
}
