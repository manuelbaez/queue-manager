using System;
using System.Collections.Generic;
using System.Text;

namespace QueueManager.RabbitMq.Constants
{
    public struct ErrorMessages
    {
        public const string NoContentType = "No content type specified on message, message body can not be deserialized";
        public const string InvalidContentType = "Invalid content type specified on message, message body can not be deserialized";
        public const string InvalidMessageBody = "The specified message body is invalid, message body can not be deserialized";

    }
}
