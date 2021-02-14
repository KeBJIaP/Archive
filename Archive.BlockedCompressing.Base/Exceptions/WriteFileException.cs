using System;
using System.Runtime.Serialization;

namespace Archive.BlockedCompressing.Base.Exceptions
{
    public class WriteFileException : Exception
    {
        public WriteFileException() : this("Ошибка при записи в выходной файл")
        {
        }

        public WriteFileException(string message) : base(message)
        {
        }

        public WriteFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WriteFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
