using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace CRZ.Framework.Web.Http
{
    public sealed class HttpServiceBuilder : IHttpServiceBuilder
    {
        private readonly IHttpClientBuilder _httpClientBuilder;

        internal HttpServiceBuilder(IHttpClientBuilder httpClientBuilder)
        {
            _httpClientBuilder = httpClientBuilder ?? throw new ArgumentNullException(nameof(httpClientBuilder));
        }

        public IHttpServiceBuilder SetHandlerLifetime(TimeSpan handlerLifetime)
        {
            _httpClientBuilder.SetHandlerLifetime(handlerLifetime);

            return this;
        }

        public IHttpServiceBuilder WaitAndRetry(WaitAndRetryOptions options)
        {
            _httpClientBuilder.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(
                options.WaitList
            ));

            return this;
        }

        public IHttpServiceBuilder CircuitBreaker(CircuitBreakerOptions options)
        {
            _httpClientBuilder.AddTransientHttpErrorPolicy(builder =>
                builder.CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: options.EventsAllowedBeforeBreaking,
                    durationOfBreak: options.DurationOfBreak
            ));

            return this;
        }
    }
}
