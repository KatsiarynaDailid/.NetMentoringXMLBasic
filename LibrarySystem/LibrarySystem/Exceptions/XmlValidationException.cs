using System;
using System.Runtime.Serialization;

namespace LibrarySystem.Parsers
{
    [Serializable]
    public class XmlValidationException : Exception
    {
        public XmlValidationException()
        {
        }

        public XmlValidationException(string message) : base(message)
        {
        }

        public XmlValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected XmlValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}