using System;
using System.Collections.Generic;

namespace CRZ.Framework.Web.Messages
{
    public class DefaultErrorMessage
    {
        public int StatusCode { get; }

        public IEnumerable<ResponseMessage> Messages { get; }

        public DefaultErrorMessage(int statusCode, IEnumerable<ResponseMessage> messages)
        {
            StatusCode = statusCode;
            Messages = messages;
        }

        public DefaultErrorMessage(int statusCode, Exception ex)
            : this(statusCode, new[] { ResponseMessage.Create(ex) })
        { }
    }
}
