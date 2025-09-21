using AggregationApi.Interfaces;
using AggregationApi.Models.SpotifyDTO;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;

namespace AggregationApi.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly HttpClient _httpClient;
        private string _accessToken;
        private DateTime _tokenExpiry;
        private readonly ILogger<SpotifyService> _logger;

        public SpotifyService(HttpClient httpClient, IConfiguration configuration, ILogger<SpotifyService> logger)
        {
            _clientId = configuration["ClientId"];
            _clientSecret = configuration["ClientSecret"];
            _httpClient = new HttpClient();
            _logger = logger;
        }

        // Get or refresh access token
        private async Task<string> GetAccessTokenAsync()
        {
            if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiry)
            {
                return _accessToken;
            }

            var tokenUrl = "https://accounts.spotify.com/api/token";

            var parameters = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", _clientId },
            { "client_secret", _clientSecret }
        };

            var content = new FormUrlEncodedContent(parameters);
            var response = await _httpClient.PostAsync(tokenUrl, content);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(jsonString);

            _accessToken = jsonDoc.RootElement.GetProperty("access_token").GetString();
            int expiresIn = jsonDoc.RootElement.GetProperty("expires_in").GetInt32();
            _tokenExpiry = DateTime.UtcNow.AddSeconds(expiresIn - 60); // refresh 1 min early

            return _accessToken;
        }

        // Generic method to call any Spotify endpoint
        public async Task<List<SpotifyTrackDTO>> GetNewReleasesAsync(int limit)
        {
            try
            {
                var token = await GetAccessTokenAsync();
                // Example: Top tracks globally
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/browse/new-releases?limit={limit}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var spotifyResponse = await response.Content.ReadFromJsonAsync<SpotifyApiResponse>();

                return spotifyResponse?.Albums.Items.Select(a => new SpotifyTrackDTO
                {
                    Artist = a.Artists.FirstOrDefault()?.Name,
                    Album = a.Name                  
                }).ToList() ?? new List<SpotifyTrackDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch Spotify tracks");
                return new List<SpotifyTrackDTO>();
            }
        }
    }
}
