using System;

namespace CRZ.Framework.Web.Http
{
    public interface IHttpServiceBuilder
    {
        IHttpServiceBuilder SetHandlerLifetime(TimeSpan handlerLifetime);
        IHttpServiceBuilder WaitAndRetry(WaitAndRetryOptions options);
        IHttpServiceBuilder CircuitBreaker(CircuitBreakerOptions options);
    }
}
