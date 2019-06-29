using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;

namespace CRZ.Framework.Cloud.GCP.Storage
{
    public class StorageService
    {
        readonly IGCPConfiguration _gcpConfiguration;
        readonly StorageClient _storageClient;

        public StorageService(IGCPConfiguration gcpConfiguration)
        {
            _gcpConfiguration = gcpConfiguration ?? throw new ArgumentNullException(nameof(gcpConfiguration));

            _storageClient = StorageClient.Create();
        }

        public async Task CreateBucket(string bucketName, CancellationToken cancellationToken = default)
        {
            await _storageClient.CreateBucketAsync(_gcpConfiguration.ProductId, bucketName, cancellationToken: cancellationToken);
        }

        public async Task DeleteBucket(string bucketName, CancellationToken cancellationToken = default)
        {
            await _storageClient.DeleteBucketAsync(bucketName, cancellationToken: cancellationToken);
        }

        public async Task UploadObject(string bucketName, string objectName, string contentType, Stream source, CancellationToken cancellationToken = default)
        {
            await _storageClient.UploadObjectAsync(bucketName, objectName, contentType, source, cancellationToken: cancellationToken);
        }

        public async Task DeleteObject(string bucketName, string objectName, CancellationToken cancellationToken = default)
        {
            await _storageClient.DeleteObjectAsync(bucketName, objectName, cancellationToken: cancellationToken);
        }
    }
}
