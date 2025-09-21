using AggregationApi.Models.SpotifyDTO;
using System.Text.Json;

namespace AggregationApi.Interfaces
{
    public interface ISpotifyService
    {
        Task<List<SpotifyTrackDTO>> GetNewReleasesAsync(int limit);
    }
}
