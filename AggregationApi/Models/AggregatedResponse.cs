using AggregationApi.Models.NewsDTO;
using AggregationApi.Models.SpotifyDTO;
using AggregationApi.Models.WeatherDTO;

namespace AggregationApi.Models
{
    public class AggregatedResponse
    {
        public string City { get; set; } = "";
        public WeatherData Weather { get; set; } = new();
        public List<Article> News { get; set; } = new();
        public List<SpotifyTrackDTO> SpotifyTopTracks { get; set; }
    }
}
