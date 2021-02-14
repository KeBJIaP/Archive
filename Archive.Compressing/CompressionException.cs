using System;
using System.Runtime.Serialization;

namespace Archive.Compressing
{
    public class CompressionException : Exception
    {
        public CompressionException() : this("Не удалосьпроизвести компрессию")
        {
        }

        public CompressionException(string message) : base(message)
        {
        }

        public CompressionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CompressionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
