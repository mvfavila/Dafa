using DAFA.Domain.ValueObjects;

namespace DAFA.Domain.Interfaces.Validation
{
    /// <summary>
    /// Contract for Entities that must be auto validated.
    /// </summary>
    public interface ISelfValidator
    {
        /// <summary>
        /// See <see cref="ValidationResult"/>.
        /// </summary>
        ValidationResult ValidationResult { get; }

        /// <summary>
        /// Validates the Entity and informs the validation result.
        /// </summary>
        /// <returns>Boolean that informs if the Entity is valid.</returns>
        bool IsValid(); 
    }
}