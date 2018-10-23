using System;

namespace Galore.Models.Exceptions
{
    public class LoanException : Exception
    {
        public LoanException() : base("The resource was not found.") { }
        public LoanException(string message) : base(message) { }
        public LoanException(string message, Exception inner) : base(message, inner) { }
    }
}