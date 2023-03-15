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

        public ResourceNotFoundException(Type type)
        {
            this.type = type;
        }

        public ResourceNotFoundException(string? message) : base(message)
        {
        }

        public ResourceNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}