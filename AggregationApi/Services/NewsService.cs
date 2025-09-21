using AggregationApi.Interfaces;
using AggregationApi.Models.NewsDTO;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;

namespace AggregationApi.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private string _apiKey;
        private readonly ILogger<NewsService> _logger;

        public NewsService(HttpClient httpClient, IConfiguration configuration, ILogger<NewsService> logger)
        {
            _httpClient = httpClient;
            _apiKey = configuration["NewsApiKey"];
            _logger = logger;
        }

        public async Task<NewsResponse> GetNewsAsync(string country, string newsSortBy)
        {
            try
            {
                var builder = new UriBuilder("https://newsapi.org/v2/top-headlines");
                var query = HttpUtility.ParseQueryString(builder.Query);

                query["country"] = country;
                query["apiKey"] = _apiKey;
              

                builder.Query = query.ToString();

                var requestUrl = builder.ToString();
                var newsResponse = await _httpClient.GetFromJsonAsync<NewsResponse>(requestUrl);

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