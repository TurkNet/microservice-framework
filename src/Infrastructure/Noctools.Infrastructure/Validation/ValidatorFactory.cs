using System;
using FluentValidation;

namespace Noctools.Infrastructure.Validation
{
    /// <summary>
    /// Utility that returns a validator for a model.
    /// </summary>
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly IServiceProvider _context;

        /// <summary>
        /// Initializes an instance of the <see cref="CheckoutValidatorFactory"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IServiceProvider"/> for the application.</param>
        public ValidatorFactory(IServiceProvider context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a validator for a model.
        /// </summary>
        /// <param name="validatorType">The <c>Type</c> that a validator needs to be created for.</param>
        /// <returns>A <see cref="IValidator"/> for the given type.</returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            var instance = _context.GetService(validatorType);
            var validator = instance as IValidator;
            return validator;
        }
    }
}