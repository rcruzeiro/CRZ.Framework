using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CRZ.Framework.Cloud.GCP
{
    public class DefaultGCPConfiguration : IGCPConfiguration
    {
        protected string SectionName { get; }

        public string ProductId { get; }

        public string ComputeZone { get; }

        public DefaultGCPConfiguration(string productId, string computeZone)
        {
            ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
            ComputeZone = computeZone ?? throw new ArgumentNullException(nameof(computeZone));
        }

        public DefaultGCPConfiguration(IConfiguration configuration, string sectionName = "GCP")
        {
            SectionName = sectionName;

            new ConfigureFromConfigurationOptions<DefaultGCPConfiguration>(
                configuration.GetSection(SectionName)).Configure(this);
        }
    }
}
