using System;
using System.Runtime.Serialization;

namespace SafetyFund.Business
{
    [Serializable]
    public class UserAlreadyVotedException : Exception
    {
        public UserAlreadyVotedException()
        {
        }

        public UserAlreadyVotedException(string message) : base(message)
        {
        }

        public UserAlreadyVotedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserAlreadyVotedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}