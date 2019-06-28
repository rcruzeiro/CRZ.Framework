using System.Net;

namespace CRZ.Framework.Cloud.AWS.S3.Models
{
    public class S3Response
    {
        public HttpStatusCode StatusCode { get; internal set; }

        public string Message { get; internal set; }
    }
}
