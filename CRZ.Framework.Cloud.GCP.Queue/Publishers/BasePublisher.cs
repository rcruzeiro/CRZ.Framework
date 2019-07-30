using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Grpc.Core;
using Newtonsoft.Json;

namespace CRZ.Framework.Cloud.GCP.Queue.Publishers
{
    public abstract class BasePublisher : IPublisher
    {
        protected string ProjectId { get; }

        protected PublisherServiceApiClient Publisher { get; }

        protected BasePublisher(IGCPConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            ProjectId = configuration.ProjectId;
            Publisher = PublisherServiceApiClient.Create();
        }

        public async Task CreateTopic(string topicName, CancellationToken cancellationToken = default)
        {
            try
            {
                var topic = new TopicName(ProjectId, topicName);
                await Publisher.CreateTopicAsync(topic, cancellationToken);
            }
            catch (RpcException e)
            when (e.Status.StatusCode == StatusCode.AlreadyExists ||
                  e.Status.StatusCode == StatusCode.PermissionDenied)
            {
                return;
            }
        }

        public async Task Publish<T>(string topicName, IEnumerable<T> data, CancellationToken cancellationToken = default)
            where T : class
        {
            var messages = new List<PubsubMessage>();
            var topic = new TopicName(ProjectId, topicName);

            data.ToList().ForEach(d => messages.Add(new PubsubMessage
            {
                Data = ByteString.CopyFromUtf8(JsonConvert.SerializeObject(d))
            }));

            await Publisher.PublishAsync(topic, messages, cancellationToken);
        }
    }
}