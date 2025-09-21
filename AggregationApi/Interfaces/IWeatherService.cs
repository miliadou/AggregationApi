using AggregationApi.Models.WeatherDTO;

namespace AggregationApi.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(string city);
    }
}
