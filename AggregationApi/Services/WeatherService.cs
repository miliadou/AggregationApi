using AggregationApi.Interfaces;
using AggregationApi.Models.NewsDTO;
using AggregationApi.Models.WeatherDTO;
using System.Net.Http.Headers;
using System.Text;

namespace AggregationApi.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private string _apiKey;      
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _apiKey = configuration["WeatherApiKey"];         
            _logger = logger;
        }

        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
                var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(url);

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
