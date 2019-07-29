using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;

namespace CRZ.Framework.Cloud.GCP.Storage
{
    public class StorageService : IDisposable
    {
        protected IGCPConfiguration GCPConfiguration { get; }

        protected StorageClient StorageClient { get; }

        public StorageService(IGCPConfiguration gcpConfiguration)
        {
            GCPConfiguration = gcpConfiguration ?? throw new ArgumentNullException(nameof(gcpConfiguration));
            StorageClient = StorageClient.Create();
        }

        public async Task CreateBucket(string bucketName, CancellationToken cancellationToken = default)
        {
            await StorageClient.CreateBucketAsync(GCPConfiguration.ProjectId, bucketName, cancellationToken: cancellationToken);
        }

        public async Task DeleteBucket(string bucketName, CancellationToken cancellationToken = default)
        {
            await StorageClient.DeleteBucketAsync(bucketName, cancellationToken: cancellationToken);
        }

        public async Task UploadObject(string bucketName, string objectName, string contentType, Stream source, CancellationToken cancellationToken = default)
        {
            await StorageClient.UploadObjectAsync(bucketName, objectName, contentType, source, cancellationToken: cancellationToken);
        }

        public async Task DeleteObject(string bucketName, string objectName, CancellationToken cancellationToken = default)
        {
            await StorageClient.DeleteObjectAsync(bucketName, objectName, cancellationToken: cancellationToken);
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
                StorageClient.Dispose();
            }
        }
    }
}
