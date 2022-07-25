using FluentValidation.Results;

namespace Noctools.Infrastructure.Validation
{
    /// <summary>
    /// Used to validate models.
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Validates the given model.
        /// </summary>
        /// <typeparam name="T">The model <c>Type</c> to validate.</typeparam>
        /// <param name="entity">The model to validate.</param>
        /// <returns>The <c>ValidationResult</c> for the model.</returns>
        ValidationResult Validate<T>(T entity) where T : class;

        /// <summary>
        /// Validates the given model.
        /// </summary>
        /// <typeparam name="T">The model <c>Type</c> to validate.</typeparam>
        /// <param name="entity">The model to validate.</param>
        /// <returns>The <c>ValidationResult</c> for the model.</returns>
        void ValidateAndThrow<T>(T entity) where T : class;
    }
}