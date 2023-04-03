using System.Runtime.Serialization;

namespace Cabanoss.Core.Exceptions
{
    [Serializable]
    internal class ResourceNotFoundException : Exception
    {
        private Type type;

        public ResourceNotFoundException()
        {
        }

        private ResourceNotFoundException(Type type)
        {
            this.type = type;
        }

        public ResourceNotFoundException(string? message) : base(message)
        {
        }

        private ResourceNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        private ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}