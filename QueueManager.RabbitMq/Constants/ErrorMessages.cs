using System;
using System.Collections.Generic;
using System.Text;

namespace QueueManager.RabbitMq.Constants
{
    public struct ErrorMessages
    {
        public const string NoContentType = "No content type specified on message, message body can not be deserialized";
        public const string InvalidContentType = "Invalid content type specified on message, message body can not be deserialized";
    }
}
