using AggregationApi.Models.NewsDTO;

namespace AggregationApi.Interfaces
{
    public interface INewsService
    {
        Task<NewsResponse> GetNewsAsync(string country, string newsSortBy);
    }
}
