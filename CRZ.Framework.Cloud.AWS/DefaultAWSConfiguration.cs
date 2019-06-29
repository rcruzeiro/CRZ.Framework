using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CRZ.Framework.Cloud.AWS
{
    public class DefaultAWSConfiguration : IAWSConfiguration
    {
        protected string SectionName { get; }

        public string AccountName { get; protected set; }

        public string SecretKey { get; protected set; }

        public string AccessKey { get; protected set; }

        public string DefaultRegion { get; protected set; }

        public DefaultAWSConfiguration(string accountName, string secretKey, string accessKey, string defaultRegion)
        {
            AccountName = accountName ?? throw new ArgumentNullException(nameof(accountName));
            SecretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
            AccessKey = accessKey ?? throw new ArgumentNullException(nameof(accessKey));
            DefaultRegion = defaultRegion ?? throw new ArgumentNullException(nameof(defaultRegion));
        }

        public DefaultAWSConfiguration(IConfiguration configuration, string sectionName = "AWS")
        {
            SectionName = sectionName;

            new ConfigureFromConfigurationOptions<DefaultAWSConfiguration>(
                configuration.GetSection(SectionName)).Configure(this);
        }
    }
}
