using System;

namespace Galore.Models.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException() : base("The resource was not found.") { }
        public AlreadyExistException(string message) : base(message) { }
        public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
    }
}