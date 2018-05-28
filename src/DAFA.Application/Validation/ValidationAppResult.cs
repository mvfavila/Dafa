using System.Collections.Generic;

namespace DAFA.Application.Validation
{
    public class ValidationAppResult
    {
        private readonly List<ValidationAppError> _errors = new List<ValidationAppError>();

        public string Message { get; set; }
        public bool IsValid 
        { 
            get { return _errors.Count == 0; }
            set
            {
                var b = value;
            }
        }

        public ICollection<ValidationAppError> Errors { get { return this._errors; } }
    }
}
