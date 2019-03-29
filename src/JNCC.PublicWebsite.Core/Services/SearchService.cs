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

        public SearchModel Query(string query, int size, int start, string siteFilter = "")
        {
            return Task.Run(() => QueryAsync(query, size, start)).Result;
        }

        public async Task<SearchModel> QueryAsync(string query, int size, int start, string siteFilter = "")
        {
            var q = new
            {
                from = start,
                size = size,
                highlight = new
                {
                    pre_tags = new[] { "<strong>" },
                    post_tags = new[] { "</strong>" },
                    fields = new
                    {
                        content = new
                        {
                            number_of_fragments = 1,
                            order = "score",
                            type = "fvh"
                        },
                        title = new
                        {

                        }
                    }
                },
                _source = new
                {
                    includes = new[] { "*" },
                    excludes = new[] { "content" }
                },
                query = new
                {
                    @bool = new
                    {
                        should = new object[] {
                            new {
                                common = new {
                                    content = new {
                                        query = query,
                                        cutoff_frequency = 0.001,
                                        low_freq_operator = "or"
                                    }
                                }
                            },
                            new {
                                common = new {
                                    title = new {
                                        query = query,
                                        cutoff_frequency = 0.001,
                                        low_freq_operator = "or"
                                    }
                                }
                            }
                        },
                        filter = GetQuerySiteFilter(siteFilter),
                        minimum_should_match = 1
                    }
                }
            };

            var serializedQuery = JsonConvert.SerializeObject(q, Formatting.None);

            return await PerformSearchAsync(serializedQuery);
        }

        private object GetQuerySiteFilter(string siteFilter)
        {
            if (string.IsNullOrWhiteSpace(siteFilter))
            {
                return null;
            }

            return new[] {
                new {
                    match = new {
                        site = new {
                            query = siteFilter
                        }
                    }
                }
            };
        }

        public SearchModel EsGetByRawQuery(string query)
        {
            return Task.Run(() => EsGetByRawQueryAsync(query)).Result;
        }

        public async Task<SearchModel> EsGetByRawQueryAsync(string rawQuery)
        {
            return await PerformSearchAsync(rawQuery);
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

        private async Task<HttpRequestMessage> GetSignedRequest(HttpRequestMessage request)
        {
            var signer = new AWS4RequestSigner(_searchConfiguration.AWSESAccessKey, _searchConfiguration.AWSESSecretKey);
            return await signer.Sign(request, "es", _searchConfiguration.AWSESRegion);
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
