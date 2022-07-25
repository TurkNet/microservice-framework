using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using ValidationException = Noctools.Domain.ValidationException;

namespace Noctools.Infrastructure.Validation
{
    /// <summary>
    /// Service used to validate a model.
    /// </summary>
    public class FluentValidationService : IValidationService
    {
        private readonly IValidatorFactory _validatorFactory;

        /// <summary>
        /// Intializes an instance of the <see cref="FluentValidationService"/> class.
        /// </summary>
        /// <param name="validatorFactory">The factory class used to build the validator.</param>
        public FluentValidationService(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        /// <summary>
        /// Validates a model.
        /// </summary>
        /// <typeparam name="T">The model <c>Type</c>.</typeparam>
        /// <param name="entity">The model to validate.</param>
        /// <returns>A <c>ValidationResult</c> for the given model.</returns>
        public ValidationResult Validate<T>(T entity) where T : class
        {
            var validator = _validatorFactory.GetValidator(entity.GetType());
            var result = validator.Validate(entity);
            return result;
        }

        public void ValidateAndThrow<T>(T entity) where T : class
        {
            var validator = _validatorFactory.GetValidator(entity.GetType());
            var result = validator.Validate(entity);
            if (result == null || result.IsValid || result.Errors == null || !result.Errors.Any()) return;
            var error = result.Errors.First();
            throw new ValidationException(error.ErrorCode, error.ErrorMessage, string.Empty);
        }
    }
}