using System;
using System.Runtime.Serialization;

namespace SafetyFund.Web.Controllers
{
    [Serializable]
    public class InvalidAuthStatusException : Exception
    {
        public InvalidAuthStatusException()
        {
        }

        public InvalidAuthStatusException(string message) : base(message)
        {
        }

        public InvalidAuthStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidAuthStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}