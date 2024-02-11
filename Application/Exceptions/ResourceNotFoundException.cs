using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    internal class ResourceNotFoundException : Exception
    {
        private Type _type;

        public ResourceNotFoundException()
        {
        }

        private ResourceNotFoundException(Type type)
        {
            this._type = type;
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