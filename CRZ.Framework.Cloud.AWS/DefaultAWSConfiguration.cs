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
            AccountName = accountName;
            SecretKey = secretKey;
            AccessKey = accessKey;
            DefaultRegion = defaultRegion;
        }

        public DefaultAWSConfiguration(IConfiguration configuration, string sectionName = "AWS")
        {
            SectionName = sectionName;

            new ConfigureFromConfigurationOptions<DefaultAWSConfiguration>(
                configuration.GetSection(SectionName)).Configure(this);
        }
    }
}
