namespace AggregationApi.Models
{
    public class AggregatorRequest
    {
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string NewsSortBy { get; set; } = "publishedAt"; 
        public int SpotifyLimit { get; set; } = 5;
    }
}
