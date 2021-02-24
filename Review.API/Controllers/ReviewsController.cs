using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Review.API.Helpers;
using Review.Domain.Exceptions;
using Review.Domain.Interfaces.DataExtractor;
using Review.Domain.Models;
using Review.Infrastructure.Services;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDataExtractor _dataExtractor;
        private readonly ProductService _productService;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(ILogger<ReviewsController> logger,
            IConfiguration configuration, IDataExtractor dataExtractor, ProductService productService)
        {
            _logger = logger;
            _dataExtractor = dataExtractor;
            _config = configuration;
            _productService = productService;
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReviewsByProductIdAsync( string productId)
        {
            var urlPrefix = _config.GetValue<string>("ExternalDataInformation:SingleProductReviewsUrlPrefix");
            Product product;
            try
            {
                product = await _dataExtractor.GetExternalProductByIdAsync(productId, urlPrefix);
            }
            catch (ExternalProductNotFoundException)
            {
                _logger.Log(LogLevel.Error, string.Format(ErrorMessageHelper.ExternalProductErrorMessage, productId));
                return NotFound();
            }
            
            await _productService.CreateAsync(product);
            
            return Ok(product);
        }
        
        [HttpGet("history/list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<IActionResult> GetReviewsAsync([FromQuery] string sort = "desc", [FromQuery] int size = 10)
        {
            var products = await _productService.GetListAsync(sort, size);
            return Ok(products);
        }
    }
}