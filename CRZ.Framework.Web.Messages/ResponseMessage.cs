using System;

namespace CRZ.Framework.Web.Messages
{
    public sealed class ResponseMessage
    {
        public string Code { get; private set; }

        public string Source { get; private set; }

        public string Message { get; private set; }

        public string InnerException { get; private set; }

        public string StackTrace { get; private set; }

        private ResponseMessage()
        { }

        public static ResponseMessage Create(Exception ex, string code = null)
        {
            return new ResponseMessage
            {
                Code = code,
                InnerException =
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty,
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace
            };
        }

        public static ResponseMessage Create(string code, string message)
        {
            return new ResponseMessage
            {
                Code = code,
                Message = message
            };
        }
    }
}
