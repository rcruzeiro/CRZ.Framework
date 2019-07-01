using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using CRZ.Framework.Cloud.AWS.S3.Models;

namespace CRZ.Framework.Cloud.AWS.S3
{
    public class S3Service : IDisposable
    {
        protected IAmazonS3 S3Client { get; }

        public S3Service(IAWSConfiguration awsConfiguration)
        {
            if (awsConfiguration == null) throw new ArgumentNullException(nameof(awsConfiguration));

            Enum.TryParse(typeof(RegionEndpoint), awsConfiguration.DefaultRegion, true, out object result);

            S3Client = new AmazonS3Client(awsConfiguration.AccessKey, awsConfiguration.SecretKey, (RegionEndpoint)result);
        }

        public async Task<S3Response> CreateBucket(string bucketName, CancellationToken cancellationToken = default)
        {
            if (await DoesS3BucketExist(bucketName) == false)
            {
                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };

                var awsResponse = await S3Client.PutBucketAsync(putBucketRequest, cancellationToken);
                var s3Response = new S3Response
                {
                    StatusCode = awsResponse.HttpStatusCode,
                    Message = awsResponse.ResponseMetadata.RequestId
                };

                return s3Response;
            }

            return null;
        }

        async Task<bool> DoesS3BucketExist(string bucketName)
        {
            return await AmazonS3Util.DoesS3BucketExistAsync(S3Client, bucketName);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (S3Client != null)
                    S3Client.Dispose();
            }
        }
    }
}
