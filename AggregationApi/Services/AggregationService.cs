using AggregationApi.Interfaces;
using AggregationApi.Models;
using AggregationApi.Models.NewsDTO;
using AggregationApi.Models.SpotifyDTO;
using AggregationApi.Models.WeatherDTO;

namespace AggregationApi.Services
{
    public class AggregationService
    {
        private readonly IWeatherService _weatherService;
        private readonly INewsService _newsService;
        private readonly ISpotifyService _spotifyService;
        private readonly ILogger<AggregationService> _logger;

        public AggregationService(IWeatherService weatherService, INewsService newsService, ISpotifyService spotifyService, ILogger<AggregationService> logger)
        {
            _weatherService = weatherService;
            _newsService = newsService;
            _spotifyService = spotifyService;
            _logger = logger;
        }

        public async Task<AggregatedResponse> GetAggregatedData(AggregatorRequest request)
        {
            WeatherData? weather = null;
            List<Article> news = new List<Article>();
            List<SpotifyTrackDTO> spotify = new List<SpotifyTrackDTO>();

            try
            {
                var weatherTask = SafeExecuteAsync(() => _weatherService.GetWeatherAsync(request.City));
                var newsTask = SafeExecuteAsync(() => _newsService.GetNewsAsync(request.CountryCode, request.NewsSortBy));
                var spotifyTask = SafeExecuteAsync(() => _spotifyService.GetNewReleasesAsync(request.SpotifyLimit));

                await Task.WhenAll(weatherTask, newsTask, spotifyTask);

                weather = weatherTask.Result;
                news = newsTask.Result?.Articles ?? new List<Article>();
                spotify = spotifyTask.Result ?? new List<SpotifyTrackDTO>();

                if (news.Any())
                {
                    if (request.NewsSortBy == "publishedAt")
                    {
                        news = news.OrderByDescending(n => n.PublishedAt).ToList();
                    }                  
                    else if (request.NewsSortBy == "author")
                    {
                        news = news.OrderBy(n => n.Author ?? string.Empty).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while aggregating data");
            }

            return new AggregatedResponse
            {
                City = request.City,
                Weather = weather,
                News = news,
                SpotifyTopTracks = spotify
            };
        }

        private async Task<T> SafeExecuteAsync<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Transient API error, returning default value.");
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API failed, returning default value.");
                return default;
            }
        }
    }


}
