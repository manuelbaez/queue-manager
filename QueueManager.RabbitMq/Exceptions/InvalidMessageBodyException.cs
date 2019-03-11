using QueueManager.RabbitMq.Constants;
using System;

namespace QueueManager.RabbitMq.Exceptions
{
    public class InvalidMessageBodyException : Exception
    {
        public InvalidMessageBodyException() : base(ErrorMessages.InvalidMessageBody)
        {

        }
    }
}
