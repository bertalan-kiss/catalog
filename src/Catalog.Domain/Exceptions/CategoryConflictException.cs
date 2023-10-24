using System.Runtime.Serialization;

namespace Catalog.Domain.Exceptions
{
    public class CategoryConflictException : Exception
    {
        public CategoryConflictException()
        {
        }

        public CategoryConflictException(string? message) : base(message)
        {
        }

        public CategoryConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CategoryConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

