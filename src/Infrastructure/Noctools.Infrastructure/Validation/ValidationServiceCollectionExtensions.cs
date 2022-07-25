using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Noctools.Infrastructure.Validation
{
    public static class ValidationServiceCollectionExtensions
    {
        public static IServiceCollection AddValidationService(this IServiceCollection services,
            IEnumerable<Assembly> registeredAssemblies)
        {
            services.AddValidators(registeredAssemblies);
            services.TryAddSingleton<IValidatorFactory, ValidatorFactory>();
            services.TryAddSingleton<IValidationService, FluentValidationService>();
            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services,
            IEnumerable<Assembly> registeredAssemblies)
        {
            var validators = registeredAssemblies.SelectMany(s => s.DefinedTypes.Where(x =>
                x.GetInterfaces().Where(w => w.IsGenericType)
                    .Any(a => a.Name.Contains("Validator"))));

            foreach (var validator in validators)
                services.AddSingleton(
                    validator.GetInterfaces()
                        .First(i => i.Name.Contains("Validator")), validator);

            return services;
        }
    }
}