using System.Runtime.Serialization;

namespace Catalog.Domain.Exceptions
{
    public class MessageSendoutFailedException : Exception
    {
        public MessageSendoutFailedException()
        {
        }

        public MessageSendoutFailedException(string? message) : base(message)
        {
        }

        public MessageSendoutFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MessageSendoutFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
