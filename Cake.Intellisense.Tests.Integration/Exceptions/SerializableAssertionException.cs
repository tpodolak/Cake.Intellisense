using System;
using System.Runtime.Serialization;

namespace Cake.Intellisense.Tests.Integration.Exceptions
{
    [Serializable]
    public class SerializableAssertionException : Exception
    {
        public SerializableAssertionException()
        {
        }

        public SerializableAssertionException(string message)
            : base(message)
        {
        }

        public SerializableAssertionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SerializableAssertionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}