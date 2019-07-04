using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CRZ.Framework.Web.Security
{
    public class TokenConfiguration
    {
        protected string SectionName { get; }

        public string Audience { get; }

        public string Issuer { get; }

        public int Seconds { get; }

        public TokenConfiguration(string audience, string issuer, int seconds)
        {
            Audience = audience ?? throw new ArgumentNullException(nameof(audience));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            Seconds = seconds;

            if (seconds == default)
                throw new InvalidOperationException(nameof(seconds));
        }

        public TokenConfiguration(IConfiguration configuration, string sectionName = "Token")
        {
            SectionName = sectionName;

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                configuration.GetSection(SectionName)).Configure(this);
        }
    }
}
