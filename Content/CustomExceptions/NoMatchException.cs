using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BootstrapVillas.Content.CustomExceptions
{
    public class NoMatchException : Exception
    {
        public NoMatchException() : base() { }
        public NoMatchException(string message) : base(message) { }
        public NoMatchException(string message, Exception innerException) : base(message, innerException) { }
        public NoMatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}