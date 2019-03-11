using QueueManager.RabbitMq.Constants;
using System;

namespace QueueManager.RabbitMq.Exceptions
{
    public class InvalidContentTypeException : Exception
    {
        public InvalidContentTypeException() : base(ErrorMessages.InvalidContentType)
        {

        }
    }
}
