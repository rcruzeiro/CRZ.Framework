using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using Grpc.Core;
using Newtonsoft.Json;

namespace CRZ.Framework.Cloud.GCP.Queue.Subscribers
{
    public abstract class BaseSubscriber : ISubscriber
    {
        protected string ProjectId { get; }

        protected SubscriberServiceApiClient Subscriber { get; }

        protected BaseSubscriber(IGCPConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            ProjectId = configuration.ProjectId;
            Subscriber = SubscriberServiceApiClient.Create();
        }

        public async Task Subscribe(string subscriptionName, string topicName, string pushEndpoint = null, int? ackDeadlineSeconds = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var subscription = new SubscriptionName(ProjectId, subscriptionName);
                var topic = new TopicName(ProjectId, topicName);

                await Subscriber.CreateSubscriptionAsync(subscription,
                                                         topic,
                                                         string.IsNullOrWhiteSpace(pushEndpoint) ? null : new PushConfig { PushEndpoint = pushEndpoint },
                                                         ackDeadlineSeconds,
                                                         cancellationToken);
            }
            catch (RpcException e)
            when (e.Status.StatusCode == StatusCode.AlreadyExists ||
                  e.Status.StatusCode == StatusCode.PermissionDenied)
            {
                return;
            }
            catch (RpcException e)
            when (e.Status.StatusCode == StatusCode.NotFound)
            {
                return;
            }
        }

        public async Task<Dictionary<string, T>> Pull<T>(string subscriptionName, bool? returnImmediately = null, int maxMessages = 20, CancellationToken cancellationToken = default)
            where T : class
        {
            var dic = new Dictionary<string, T>();
            var subscription = new SubscriptionName(ProjectId, subscriptionName);

            var result = await Subscriber.PullAsync(subscription, returnImmediately, maxMessages, cancellationToken);

            result.ReceivedMessages.ToList().ForEach(rm =>
            {
                // unpack the message
                var json = rm.Message.Data.ToByteArray();
                var message = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(json));

                dic.Add(rm.AckId, message);
            });

            return dic;
        }

        public async Task Acknowledge(string subscriptionName, IEnumerable<string> ackIds, CancellationToken cancellationToken = default)
        {
            var subscription = new SubscriptionName(ProjectId, subscriptionName);

            await Subscriber.AcknowledgeAsync(subscription, ackIds, cancellationToken);
        }

        public async Task DeleteSubscription(string subscriptionName, CancellationToken cancellationToken = default)
        {
            var subscription = new SubscriptionName(ProjectId, subscriptionName);

            await Subscriber.DeleteSubscriptionAsync(subscription, cancellationToken);
        }
    }
}
