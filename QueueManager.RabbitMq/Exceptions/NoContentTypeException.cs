using QueueManager.RabbitMq.Constants;
using System;

namespace QueueManager.RabbitMq.Exceptions
{
    public class NoContentTypeException : Exception
    {
        public NoContentTypeException() : base(ErrorMessages.NoContentType)
        {

        }
    }
}
