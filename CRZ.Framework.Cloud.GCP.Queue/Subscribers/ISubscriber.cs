using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CRZ.Framework.Cloud.GCP.Queue.Subscribers
{
    public interface ISubscriber
    {
        Task Subscribe(string subscriptionName, string topicName, string pushEndpoint = null, int? ackDeadlineSeconds = null, CancellationToken cancellationToken = default);
        Task<Dictionary<string, T>> Pull<T>(string subscriptionName, bool? returnImmediately = null, int maxMessages = 20, CancellationToken cancellationToken = default)
            where T : class;
        Task Acknowledge(string subscriptionName, IEnumerable<string> ackIds, CancellationToken cancellationToken = default);
        Task DeleteSubscription(string subscriptionName, CancellationToken cancellationToken = default);
    }
}
