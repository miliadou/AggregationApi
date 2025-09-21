using AggregationApi.Models;
using AggregationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AggregationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AggregationController : ControllerBase
    {
      
        private readonly ILogger<AggregationController> _logger;

        private readonly AggregationService _aggregationService;

        public AggregationController(AggregationService aggregationService)
        {
            _aggregationService = aggregationService;
        }

        /// <summary>
        /// Aggregates data from Weather, News, and Spotify APIs.
        /// </summary>
        /// <param name="request">Aggregator request with city, country, news filters, and spotify limit.</param>
        /// <returns>Aggregated response containing weather, news, and spotify tracks.</returns>
        [HttpPost("aggregator")]
        public async Task<IActionResult> Get(AggregatorRequest request)
        {
            var result = await _aggregationService.GetAggregatedData(request);
            return Ok(result);
        }
    }
}
