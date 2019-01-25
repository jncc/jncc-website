using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.ExtendedClient;
using Aws4RequestSigner;
using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class SearchService
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

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_searchConfiguration.AWSESEndpoint + _searchConfiguration.AWSESIndex + "/_search/"),
                Content = new StringContent(JsonConvert.SerializeObject(q, Formatting.None), Encoding.UTF8, "application/json")
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


        public  EsPut()
        {
            return Task.Run(() => ESPutAsync()).Result;
        }

        public async Task<> ESPutAsync()
        {
            var credentials = new BasicAWSCredentials(ConfigurationManager.AppSettings["JNCC.PublicWebsite.Core.AWS-ES-AccessKey"], ConfigurationManager.AppSettings["JNCC.PublicWebsite.Core.AWS-SecretKey"]);
            var region = RegionEndpoint.GetBySystemName(ConfigurationManager.AppSettings["JNCC.PublicWebsite.Core.AWS-Region"]);
            var s3 = new AmazonS3Client(credentials, region);
            var sqs = new AmazonSQSClient(credentials, region);
            var sqsExtendedClient = new AmazonSQSExtendedClient(sqs,
                new ExtendedClientConfiguration().WithLargePayloadSupportEnabled(s3, ConfigurationManager.AppSettings["JNCC.PublicWebsite.Core.AWS-SQS-PayloadBucket"])    
            );

            // index documents

            var simpleMessage = new
            {
                verb = "upsert",
                index = "test",
                document = new
                {
                    id = "123456789", // ID managed by Umbraco
                    site = "website", // as opposed to datahub|sac|mhc
                    title = "An example searchable document",
                    content = "This is a searchable document made purely for example purposes.",
                    url = "http://example.com/pages/123456789", // the URL of the page, for clicking through
                    keywords = new[]
                    {
                        new { vocab = "http://vocab.jncc.gov.uk/jncc-web", value = "Example" }
                    },
                    published_date = "2019-01-14",
                }
            };

            var basicResponse = await sqsExtendedClient.SendMessageAsync(ConfigurationManager.AppSettings["JNCC.PublicWebsite.Core.AWS-SQS-Endpoint"],
                JsonConvert.SerializeObject(simpleMessage, Formatting.None)
            );

            // index PDFs
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

        private IEnumerable<SearchResultViewModel> GetPagedSearchResults(SearchModel searchResults,int pageSize, int currentPage)
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
                    PublishDate = result.Source.PublishedDate
                    
                };

                viewModels.Add(viewModel);
            }
            return viewModels;

        }
    }
}
