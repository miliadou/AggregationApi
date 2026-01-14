using AggregationApi.Interfaces;
using AggregationApi.Interfaces.Models;
using AggregationApi.Models.NewsDTO;
using System.Net.Http.Json;
using System.Web;

namespace AggregationApi.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private string _apiKey;
        private readonly ILogger<NewsService> _logger;
        private ExternalApisOptions? _externalApisOptions;

        public NewsService(HttpClient httpClient, IConfiguration configuration, ILogger<NewsService> logger)
        {
            _httpClient = httpClient;
            _apiKey = configuration["NewsApiKey"];
            _logger = logger;
            _externalApisOptions = configuration.GetSection(ExternalApisOptions.Name).Get<ExternalApisOptions>();

        }

        public async Task<NewsResponse> GetNewsAsync(string country, string newsSortBy)
        {
            try
            {
                string queryToAppend = String.Concat("country=", country, "&apiKey=", _apiKey);
                var urlBuilder = new UriBuilder(_externalApisOptions.NewsApiUrl) { Query = queryToAppend };
                var newsResponse = await _httpClient.GetFromJsonAsync<NewsResponse>(urlBuilder.ToString());

                return newsResponse ?? new NewsResponse { Articles = new List<Article>() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch news data");
                return new NewsResponse { Articles = new List<Article> {} };
            }
        }
    }
}