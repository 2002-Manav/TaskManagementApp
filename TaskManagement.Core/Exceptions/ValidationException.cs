using System;
using System.Collections.Generic;

namespace TaskManagement.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public ValidationException() : base("Validation failed.") { }

        public ValidationException(IEnumerable<string> errors)
            : base("Validation failed.")
        {
            Errors = errors;
        }

        public ValidationException(string message)
            : base(message) { }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
