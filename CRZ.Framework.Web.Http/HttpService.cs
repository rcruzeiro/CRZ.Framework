using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CRZ.Framework.Web.Http
{
    public class HttpService
    {
        private readonly IServiceCollection _services;

        public HttpService(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IHttpServiceBuilder AddClient<TClient>()
            where TClient : class
        {
            var builder = _services.AddHttpClient<TClient>();

            return new HttpServiceBuilder(builder);
        }

        public IHttpServiceBuilder AddClient<TClient>(Action<HttpClient> configureClient)
            where TClient : class
        {
            var builder = _services.AddHttpClient<TClient>(configureClient);

            return new HttpServiceBuilder(builder);
        }

        public IHttpServiceBuilder AddClient<TClient, TImplementation>()
            where TClient : class
            where TImplementation : class, TClient
        {
            var builder = _services.AddHttpClient<TClient, TImplementation>();

            return new HttpServiceBuilder(builder);
        }

        public IHttpServiceBuilder AddClient<TClient, TImplementation>(Action<HttpClient> configureClient)
            where TClient : class
            where TImplementation : class, TClient
        {
            var builder = _services.AddHttpClient<TClient, TImplementation>(configureClient);

            return new HttpServiceBuilder(builder);
        }
    }
}
