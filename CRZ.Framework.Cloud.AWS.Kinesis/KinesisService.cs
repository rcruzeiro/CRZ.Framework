using System;
using Amazon;
using Amazon.Kinesis;

namespace CRZ.Framework.Cloud.AWS.Kinesis
{
    public class KinesisService
    {
        readonly IAmazonKinesis _client;

        public KinesisService(IAWSConfiguration awsConfiguration)
        {
            if (awsConfiguration == null) throw new ArgumentNullException(nameof(awsConfiguration));

            Enum.TryParse(typeof(RegionEndpoint), awsConfiguration.DefaultRegion, true, out object result);

            _client = new AmazonKinesisClient(awsConfiguration.AccessKey, awsConfiguration.SecretKey, (RegionEndpoint)result);
        }
    }
}
