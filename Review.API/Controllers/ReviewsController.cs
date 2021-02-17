using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Review.Domain.Models;
using Review.Infrastructure;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDataExtractor _dataExtractor;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(ILogger<ReviewsController> logger, IDataExtractor dataExtractor,
            IConfiguration configuration)
        {
            _logger = logger;
            _dataExtractor = dataExtractor;
            _config = configuration;
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReviewsByProductIdAsync(string productId)
        {
            var urlPrefix = _config.GetValue<string>("ExternalDataInformation:SingleProductReviewsUrlPrefix");
            var product = await _dataExtractor.GetExternalProductByIdAsync(productId, urlPrefix);
            return Ok(product);
        }
    }
}