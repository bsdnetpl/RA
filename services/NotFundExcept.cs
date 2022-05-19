using System.Runtime.Serialization;

namespace RA.services
{
    [Serializable]
    internal class NotFundExcept : Exception
    {
        public NotFundExcept()
        {
        }

        public NotFundExcept(string? message) : base(message)
        {
        }

        public NotFundExcept(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFundExcept(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}