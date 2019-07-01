using System;
using Amazon;
using Amazon.Kinesis;

namespace CRZ.Framework.Cloud.AWS.Kinesis
{
    public class KinesisService
    {
        protected IAmazonKinesis KinesisClient { get; }

        public KinesisService(IAWSConfiguration awsConfiguration)
        {
            if (awsConfiguration == null) throw new ArgumentNullException(nameof(awsConfiguration));

            Enum.TryParse(typeof(RegionEndpoint), awsConfiguration.DefaultRegion, true, out object result);

            KinesisClient = new AmazonKinesisClient(awsConfiguration.AccessKey, awsConfiguration.SecretKey, (RegionEndpoint)result);
        }
    }
}
