using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Api;
using Google.Cloud.Logging.Type;
using Google.Cloud.Logging.V2;

namespace CRZ.Framework.Cloud.GCP.Log
{
    public class LogService
    {
        protected IGCPConfiguration GCPConfiguration { get; }

        protected LoggingServiceV2Client LogClient { get; }

        public LogService(IGCPConfiguration gcpConfiguration)
        {
            GCPConfiguration = gcpConfiguration ?? throw new ArgumentNullException(nameof(gcpConfiguration));
            LogClient = LoggingServiceV2Client.Create();
        }

        public async Task Info<T>(string id, string message, IDictionary<string, string> labels = null, CancellationToken cancellationToken = default)
            where T : class
        {
            await CreateLog<T>(id, LogSeverity.Info, message, labels, cancellationToken);
        }

        public async Task Warning<T>(string id, string message, IDictionary<string, string> labels = null, CancellationToken cancellationToken = default)
            where T : class
        {
            await CreateLog<T>(id, LogSeverity.Warning, message, labels, cancellationToken);
        }

        public async Task Error<T>(string id, string message, IDictionary<string, string> labels = null, CancellationToken cancellationToken = default)
            where T : class
        {
            await CreateLog<T>(id, LogSeverity.Error, message, labels, cancellationToken);
        }

        public async Task Critical<T>(string id, string message, IDictionary<string, string> labels = null, CancellationToken cancellationToken = default)
            where T : class
        {
            await CreateLog<T>(id, LogSeverity.Critical, message, labels, cancellationToken);
        }

        public async Task Alert<T>(string id, string message, IDictionary<string, string> labels = null, CancellationToken cancellationToken = default)
            where T : class
        {
            await CreateLog<T>(id, LogSeverity.Alert, message, labels, cancellationToken);
        }

        public async Task Emergency<T>(string id, string message, IDictionary<string, string> labels = null, CancellationToken cancellationToken = default)
            where T : class
        {
            await CreateLog<T>(id, LogSeverity.Emergency, message, labels, cancellationToken);
        }

        public async Task Notice<T>(string id, string message, IDictionary<string, string> labels = null, CancellationToken cancellationToken = default)
            where T : class
        {
            await CreateLog<T>(id, LogSeverity.Notice, message, labels, cancellationToken);
        }

        protected virtual async Task CreateLog<T>(string id, LogSeverity severity, string message, IDictionary<string, string> labels = null, CancellationToken cancellationToken = default)
            where T : class
        {
            // Prepare new log entry.
            LogEntry entry = new LogEntry();
            var logId = id;
            LogName logName = new LogName(GCPConfiguration.ProjectId, logId);
            LogNameOneof logNameToWrite = LogNameOneof.From(logName);
            entry.LogName = logName.ToString();
            entry.Severity = severity;

            // Create log entry message.
            string messageId = DateTime.Now.Millisecond.ToString();
            Type type = typeof(T);
            string entrySeverity = entry.Severity.ToString().ToUpper();
            entry.TextPayload =
                $"{messageId} {entrySeverity} {type.Namespace} - {message}";

            // Set the resource type to control which GCP resource the log entry belongs to.
            MonitoredResource resource = new MonitoredResource
            {
                Type = "global"
            };

            // Add log entry to collection for writing. Multiple log entries can be added.
            IEnumerable<LogEntry> logEntries = new LogEntry[] { entry };

            // Write new log entry.
            await LogClient.WriteLogEntriesAsync(logNameToWrite, resource, labels, logEntries);
        }
    }
}
