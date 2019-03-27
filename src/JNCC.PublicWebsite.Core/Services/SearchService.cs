using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.ExtendedClient;
using Aws4RequestSigner;
using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Logging;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class SearchService : IDataHubRawQueryService, ISearchQueryService
    {
        private readonly ISearchConfiguration _searchConfiguration;

        public SearchService(ISearchConfiguration searchConfiguration)
        {
            _searchConfiguration = searchConfiguration;
        }

        public SearchModel EsGet(string q, int size, int start)
        {
            return Task.Run(() => ESGetAsync(q, size, start)).Result;
        }
        public SearchModel EsGetByRawQuery(string query)
        {
            return Task.Run(() => EsGetByRawQueryAsync(query)).Result;
        }
        public async Task<SearchModel> EsGetByRawQueryAsync(string rawQuery)
        {
            return await PerformSearchAsync(rawQuery);
        }

        public async Task<SearchModel> ESGetAsync(string query, int size, int start)
        {
            var q = new
            {
                from = start,
                size,
                query = new
                {
                    @bool = new
                    {
                        must = new[] {
                            new {
                                common = new {
                                    content = new {
                                        query,
                                        cutoff_frequency = 0.001,
                                        low_freq_operator = "or"
                                    }
                                }
                            }
                        },
                        should = new[] {
                            new {
                                common = new {
                                    title = new {
                                        query,
                                        cutoff_frequency = 0.001,
                                        low_freq_operator = "or"
                                    }
                                }
                            }
                        }
                    }
                },
                highlight = new
                {
                    fields = new { content = new { } }
                }
            };

            var serializedQuery = JsonConvert.SerializeObject(q, Formatting.None);

            return await PerformSearchAsync(serializedQuery);
        }

        private async Task<SearchModel> PerformSearchAsync(string query)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_searchConfiguration.AWSESEndpoint + _searchConfiguration.AWSESIndex + "/_search/"),
                Content = new StringContent(query, Encoding.UTF8, "application/json")
            };

            var signedRequest = await GetSignedRequest(request);
            var response = await new HttpClient().SendAsync(signedRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SearchModel>(responseString);
        }

        internal async Task<HttpRequestMessage> GetSignedRequest(HttpRequestMessage request)
        {
            var signer = new AWS4RequestSigner(_searchConfiguration.AWSESAccessKey, _searchConfiguration.AWSESSecretKey);
            return await signer.Sign(request, "es", _searchConfiguration.AWSESRegion);
        }


        public void UpdateIndex(int pageId, string nodeName, DateTime publishDate, string url, string mainContent)
        {
            Task.Run(() => UpdateIndexAsync(pageId, nodeName, publishDate, url, mainContent));
        }

        public void UpdateIndex(int pageId, string nodeName, DateTime publishDate, string url, string filePath, string nodeExtension, string nodeBytes)
        {
            Task.Run(() => UpdateIndexAsync(pageId, nodeName, publishDate, url, filePath, nodeExtension, nodeBytes));
        }

        public async Task UpdateIndexAsync(int pageId, string nodeName, DateTime publishDate, string url, string mainContent)
        {
            if (!_searchConfiguration.EnableIndexing)
            {
                LogHelper.Info<SearchService>("Skipping indexing for Content name " + nodeName + " with ID " + pageId + ". EnableIndexing is disabled.");
                return;
            }

            if (string.IsNullOrWhiteSpace(mainContent))
            {
                LogHelper.Info<SearchService>("Skipping indexing for Content name " + nodeName + " with ID " + pageId + ". Content is Null or WhiteSpace.");
                return;
            }

            var credentials = new BasicAWSCredentials(_searchConfiguration.AWSSQSAccessKey, _searchConfiguration.AWSSQSSecretKey);
            var region = RegionEndpoint.GetBySystemName(_searchConfiguration.AWSESRegion);
            var s3 = new AmazonS3Client(credentials, region);
            var sqs = new AmazonSQSClient(credentials, region);
            var sqsExtendedClient = new AmazonSQSExtendedClient(sqs,
                new ExtendedClientConfiguration().WithLargePayloadSupportEnabled(s3, _searchConfiguration.AWSSQSPayloadBucket)
            );

            try
            {
                // this is content
                var simpleMessage = new
                {
                    verb = SearchIndexingVerbs.Upsert,
                    index = _searchConfiguration.AWSESIndex,
                    document = new
                    {
                        id = pageId.ToString(), // ID managed by Umbraco
                        site = SearchIndexingSites.Website,
                        title = nodeName,
                        content = mainContent,
                        url = url, // the URL of the page, for clicking through
                        keywords = new[]
                            {
                        new { vocab = "http://vocab.jncc.gov.uk/jncc-web", value = "Example" }
                                },
                        published_date = publishDate.ToString("yyyy-MM-dd"),

                    }
                };

                var basicResponse = await sqsExtendedClient.SendMessageAsync(_searchConfiguration.AWSSQSEndpoint,
                JsonConvert.SerializeObject(simpleMessage, Formatting.None));

                if (basicResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    LogHelper.Info<SearchService>("Content name " + nodeName + " with ID " + pageId + " has been pushed up to SQS");
                }
                else
                {
                    LogHelper.Info<SearchService>("Content name " + nodeName + " with ID " + pageId + " failed to push up to SQS. MD5 of message attributes: " + basicResponse.MD5OfMessageAttributes + "MD5 of message body:" + basicResponse.MD5OfMessageBody);
                }
            }

            catch (Exception ex)
            {
                LogHelper.Error<SearchService>("Node name " + nodeName + " with ID " + pageId + " failed pushing to SQS", ex);
            }
        }

        public async Task UpdateIndexAsync(int pageId, string nodeName, DateTime publishDate, string url, string filePath, string nodeExtension, string nodeBytes)
        {
            if (!_searchConfiguration.EnableIndexing)
            {
                LogHelper.Info<SearchService>("Skipping indexing for Media name " + nodeName + " with ID " + pageId + ". EnableIndexing is disabled.");
                return;
            }
            var credentials = new BasicAWSCredentials(_searchConfiguration.AWSSQSAccessKey, _searchConfiguration.AWSSQSSecretKey);
            var region = RegionEndpoint.GetBySystemName(_searchConfiguration.AWSESRegion);
            var s3 = new AmazonS3Client(credentials, region);
            var sqs = new AmazonSQSClient(credentials, region);
            var sqsExtendedClient = new AmazonSQSExtendedClient(sqs,
                new ExtendedClientConfiguration().WithLargePayloadSupportEnabled(s3, _searchConfiguration.AWSSQSPayloadBucket)
            );

            try
            {
                var pdf = System.IO.File.ReadAllBytes(filePath);
                var pdfEncoded = Convert.ToBase64String(pdf);

                var simpleMessage = new
                {
                    verb = SearchIndexingVerbs.Upsert,
                    index = _searchConfiguration.AWSESIndex,
                    document = new
                    {
                        id = pageId.ToString(), // ID managed by Umbraco
                        site = SearchIndexingSites.Website,
                        title = nodeName,
                        content = "Umbraco Media Content",
                        url = url, // the URL of the page, for clicking through
                        keywords = new[]
                            {
                        new { vocab = "http://vocab.jncc.gov.uk/jncc-web", value = "Example" }
                                },
                        published_date = publishDate.ToString("yyyy-MM-dd"),
                        file_base64 = pdfEncoded, // base-64 encoded file
                        file_extension = nodeExtension,   // when this is a downloadable
                        file_bytes = nodeBytes,   // file such as a PDF, etc.

                    }
                };

                var basicResponse = await sqsExtendedClient.SendMessageAsync(_searchConfiguration.AWSSQSEndpoint,
                JsonConvert.SerializeObject(simpleMessage, Formatting.None));

                if (basicResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    LogHelper.Info<SearchService>("Media name " + nodeName + " with ID " + pageId + " has been pushed up to SQS");
                }
                else
                {
                    LogHelper.Info<SearchService>("Media name " + nodeName + " with ID " + pageId + " has been pushed up to SQS. MD5 of message attributes: " + basicResponse.MD5OfMessageAttributes + "MD5 of message body:" + basicResponse.MD5OfMessageBody);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<SearchService>("Node name " + nodeName + " with ID " + pageId + " failed pushing to SQS", ex);
            }


        }

        public void DeleteFromIndex(string pageId)
        {
            Task.Run(() => DeleteFromIndexAsync(pageId));
        }

        public async Task DeleteFromIndexAsync(string pageId)
        {
            if (!_searchConfiguration.EnableIndexing)
            {
                LogHelper.Info<SearchService>("Skipping indexing for ID " + pageId + ". EnableIndexing is disabled.");
                return;
            }
            var credentials = new BasicAWSCredentials(_searchConfiguration.AWSSQSAccessKey, _searchConfiguration.AWSSQSSecretKey);
            var region = RegionEndpoint.GetBySystemName(_searchConfiguration.AWSESRegion);
            var s3 = new AmazonS3Client(credentials, region);
            var sqs = new AmazonSQSClient(credentials, region);
            var sqsExtendedClient = new AmazonSQSExtendedClient(sqs,
                new ExtendedClientConfiguration().WithLargePayloadSupportEnabled(s3, _searchConfiguration.AWSSQSPayloadBucket)
            );
            try
            {
                var simpleMessage = new
                {
                    verb = SearchIndexingVerbs.Delete,
                    index = _searchConfiguration.AWSESIndex,
                    document = new
                    {
                        id = pageId, // ID managed by Umbraco
                        site = SearchIndexingSites.Website
                    }
                };

                var basicResponse = await sqsExtendedClient.SendMessageAsync(_searchConfiguration.AWSSQSEndpoint,
                    JsonConvert.SerializeObject(simpleMessage, Formatting.None)
                );

                if (basicResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    LogHelper.Info<SearchService>("Document name with ID " + pageId + " has been pushed up to SQS for deletion");
                }
                else
                {
                    LogHelper.Info<SearchService>("Document name with ID " + pageId + " has been pushed up to SQS for deletion. MD5 of message attributes: " + basicResponse.MD5OfMessageAttributes + "MD5 of message body:" + basicResponse.MD5OfMessageBody);
                }
            }

            catch (Exception ex)
            {
                LogHelper.Error<SearchService>("Document with ID " + pageId + " failed pushing to SQS for deletion", ex);
            }
        }

        public SearchViewModel GetViewModel(SearchModel searchResults, string searchTerm, int pageSize, int currentPage)
        {
            var viewModel = new SearchViewModel()
            {
                TotalResults = searchResults.Hits.Total,
                PagedResults = GetPagedSearchResults(searchResults, pageSize, currentPage),
                TotalPages = (int)Math.Ceiling((decimal)searchResults.Hits.Total / pageSize),
                PageSize = pageSize,
                SearchTerm = searchTerm,
                CurrentPage = Math.Max(1, Math.Min((int)Math.Ceiling((decimal)searchResults.Hits.Total / pageSize), currentPage))
            };

            return viewModel;
        }

        private IEnumerable<SearchResultViewModel> GetPagedSearchResults(SearchModel searchResults, int pageSize, int currentPage)
        {
            var viewModels = new List<SearchResultViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(searchResults.Hits.Results))
            {
                return viewModels;
            }

            foreach (var result in searchResults.Hits.Results)
            {
                var viewModel = new SearchResultViewModel()
                {
                    Title = result.Source.Title,
                    Content = result.Source.Content,
                    DataType = result.Source.DataType,
                    Site = result.Source.Site,
                    Url = result.Source.Url,
                    PublishDate = result.Source.PublishedDate

                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public SearchModel GetByRawQuery(string rawQuery, int numberOfItems)
        {
            var completeRawQuery = string.Format(@"
            {{
                ""from"": {0},
                ""size"": {1},
                ""query"": {2}
            }}", 0, numberOfItems, rawQuery);

            return EsGetByRawQuery(completeRawQuery);
        }
    }
}
