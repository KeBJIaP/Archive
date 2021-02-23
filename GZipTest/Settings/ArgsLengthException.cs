using System;
using System.Runtime.Serialization;

namespace GZipTest.Settings
{
    [Serializable]
    internal class ArgsLengthException : Exception
    {
        public ArgsLengthException()
        {
        }

        public ArgsLengthException(string message) : base(message)
        {
        }

        public ArgsLengthException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArgsLengthException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}