using System;

namespace Review.Domain.Exceptions
{
    public class ExternalProductNotFoundException: Exception
    {
        public ExternalProductNotFoundException()
        {
        }

        public ExternalProductNotFoundException(string message)
            : base(message)
        {
        }

        public ExternalProductNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}