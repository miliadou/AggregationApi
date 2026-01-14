namespace AggregationApi.Interfaces.Models
{
    public class ExternalApisOptions
    {
        public const string Name = "ExternalApisUrls";

        public string WeatherApiUrl { get; set; }
        public string SpotifyApiUrl { get; set; }
        public string SpotifyTokenUrl { get; set; }        
        public string NewsApiUrl { get; set; }
    }
}
