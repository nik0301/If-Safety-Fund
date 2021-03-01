using System;
using System.Runtime.Serialization;

namespace SafetyFund.Business
{
    [Serializable]
    public class InvalidSocialMediaException : Exception
    {
        public InvalidSocialMediaException()
        {
        }

        public InvalidSocialMediaException(string message) : base(message)
        {
        }

        public InvalidSocialMediaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidSocialMediaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}