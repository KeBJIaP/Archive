using System;
using System.Runtime.Serialization;

namespace GZipTest.Settings
{
    [Serializable]
    internal class ModeParseException : Exception
    {
        public ModeParseException()
        {
        }

        public ModeParseException(string message) : base(message)
        {
        }

        public ModeParseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModeParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}