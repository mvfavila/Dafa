using System.Collections.Generic;
using System.Linq;

namespace DAFA.Domain.ValueObjects
{
    /// <summary>
    /// Result of an Domain Entity Validation. Informs if the Entity is valid.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public ValidationResult()
        {
            errors = new List<ValidationError>();
        }

        /// <summary>
        /// Errors list generated during validation.
        /// </summary>
        private readonly List<ValidationError> errors;

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Informs if the ValidationResult is valid.
        /// </summary>
        public bool IsValid { get { return errors.Count == 0; } }

        /// <summary>
        /// Errors list generated during validation.
        /// </summary>
        public IEnumerable<ValidationError> Errors { get { return this.errors; } }

        /// <summary>
        /// Adds an Error to the Validation Result.
        /// </summary>
        /// <param name="error">See <see cref="ValidationError"/>.</param>
        public void AddError(ValidationError error)
        {
            errors.Add(error);
        }

        /// <summary>
        /// Adds Errors from other Validation Results to the current Validation Result.
        /// </summary>
        /// <param name="validationResults">Validation Result array.</param>
        public void AddError(params ValidationResult[] validationResults)
        {
            if (validationResults == null) return;

            foreach (var validationResult in validationResults.Where(result => result != null))
                this.errors.AddRange(validationResult.Errors);
        }

        /// <summary>
        /// Removes an Error from the Validation Result.
        /// </summary>
        /// <param name="error"></param>
        public void RemoveError(ValidationError error)
        {
            if (errors.Contains(error))
                errors.Remove(error);
        }
    }
}
