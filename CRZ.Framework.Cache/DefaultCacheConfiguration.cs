using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CRZ.Framework.Cache
{
    public class DefaultCacheConfiguration : ICacheConfiguration
    {
        protected string SectionName = "Cache";

        public string Endpoint { get; }

        public int Database { get; }

        public TimeSpan ExpirationTime { get; }

        public DefaultCacheConfiguration(string endpoint, int database, TimeSpan timeToExpire)
        {
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            Database = database;
            ExpirationTime = timeToExpire;
        }

        public DefaultCacheConfiguration(IConfiguration configuration, string sectionName = "Cache")
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            SectionName = sectionName;

            new ConfigureFromConfigurationOptions<DefaultCacheConfiguration>(
                configuration.GetSection(SectionName)).Configure(this);
        }
    }
}
