using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.ExtendedClient;
using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Umbraco.Core.Logging;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class SearchIndexingQueueService : ISearchIndexingQueueService, IDisposable
    {
        private readonly ISearchConfiguration _searchConfiguration;
        private readonly JsonSerializerSettings _jsonSettings;
        private readonly AmazonSQSExtendedClient _sqsExtendedClient;

        public SearchIndexingQueueService(ISearchConfiguration searchConfiguration)
        {
            _jsonSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            _searchConfiguration = searchConfiguration;
            _sqsExtendedClient = CreateExtendedClient();
        }

        public void QueueUpsert(SearchIndexDocumentModel document)
        {
            if (string.IsNullOrWhiteSpace(document.Content))
            {
                LogHelper.Info<SearchIndexingQueueService>("Skipping Queue Upsert for node (ID: {0}, Title: {1}). Reason: Content is Null or WhiteSpace.", () => document.NodeId, () => document.Title);
                return;
            }

            var request = new SearchIndexQueueRequestModel
            {
                Verb = SearchIndexingVerbs.Upsert,
                Index = _searchConfiguration.AWSESIndex,
                Document = document
            };

            QueueRequest(request);
        }

        public void QueueDelete(SearchIndexDocumentModel document)
        {
            var request = new SearchIndexQueueRequestModel
            {
                Verb = SearchIndexingVerbs.Delete,
                Index = _searchConfiguration.AWSESIndex,
                Document = document
            };

            QueueRequest(request);
        }

        public void Dispose()
        {
            _sqsExtendedClient.Dispose();
        }

        private void QueueRequest(SearchIndexQueueRequestModel request)
        {
            Task.Run(() => QueueRequestAsync(request));
        }

        private async Task QueueRequestAsync(SearchIndexQueueRequestModel request)
        {
            var message = JsonConvert.SerializeObject(request, Formatting.None, _jsonSettings);

            var response = await _sqsExtendedClient.SendMessageAsync(_searchConfiguration.AWSSQSEndpoint, message);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                LogHelper.Warn<SearchIndexingQueueService>("Failed to push up to SQS. Document Request (ID: {0}, Title: {1}). MD5 of message attributes: {2}. MD5 of message body: {3}.", () => request.Document.NodeId, () => request.Document.Title, () => response.MD5OfMessageAttributes, () => response.MD5OfMessageBody);
            }

            LogHelper.Info<SearchIndexingQueueService>("Document Request (ID: {0}, Title: {1}) has been pushed up to SQS.", () => request.Document.NodeId, () => request.Document.Title);
        }

        private AmazonSQSExtendedClient CreateExtendedClient()
        {
            var credentials = new BasicAWSCredentials(_searchConfiguration.AWSSQSAccessKey, _searchConfiguration.AWSSQSSecretKey);
            var region = RegionEndpoint.GetBySystemName(_searchConfiguration.AWSESRegion);
            var s3 = new AmazonS3Client(credentials, region);
            var sqs = new AmazonSQSClient(credentials, region);

            return new AmazonSQSExtendedClient(sqs,
                new ExtendedClientConfiguration().WithLargePayloadSupportEnabled(s3, _searchConfiguration.AWSSQSPayloadBucket)
            );
        }
    }
}
