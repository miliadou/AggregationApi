using AggregationApi.Interfaces;
using AggregationApi.Interfaces.Models;
using AggregationApi.Models.WeatherDTO;

namespace AggregationApi.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private string _apiKey;      
        private readonly ILogger<WeatherService> _logger;
        private ExternalApisOptions? _externalApisOptions;

        public WeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _apiKey = configuration["WeatherApiKey"];         
            _logger = logger;
            _externalApisOptions = configuration.GetSection(ExternalApisOptions.Name).Get<ExternalApisOptions>();
        }

        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            try
            {
                string queryToAppend = String.Concat("q=",city,"&appid=",_apiKey,"&units=","metric");
                var urlBuilder = new UriBuilder(_externalApisOptions.WeatherApiUrl) { Query = queryToAppend };
                var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(urlBuilder.ToString());

                return new WeatherData
                {
                    Condition = response?.Weather.FirstOrDefault()?.Main,
                    Temperature = response?.Main.Temp ?? 0,
                    Humidity = response?.Main.Humidity ?? 0,
                    WindSpeed = response?.Wind.Speed ?? 0
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch weather for {City}", city);
                return null;
            }
        }
    }
   
}
