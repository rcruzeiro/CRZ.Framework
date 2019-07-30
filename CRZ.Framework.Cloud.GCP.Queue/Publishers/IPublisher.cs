using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CRZ.Framework.Cloud.GCP.Queue.Publishers
{
    public interface IPublisher
    {
        Task CreateTopic(string topicName, CancellationToken cancellationToken = default);
        Task Publish<T>(string topicName, IEnumerable<T> data, CancellationToken cancellationToken = default)
            where T : class;
    }
}