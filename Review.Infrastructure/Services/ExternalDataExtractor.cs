using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Review.Domain.Interfaces;
using Review.Domain.Interfaces.DataExtractor;
using Review.Domain.Models;

namespace Review.Infrastructure.Services
{
    public class ExternalDataExtractor : IDataExtractor
    {
        private readonly IConfiguration _angleSharpConf = Configuration.Default.WithDefaultLoader();

        public async Task<Product> GetExternalProductByIdAsync(string productId, string urlPrefix)
        {
            var context = BrowsingContext.New(_angleSharpConf);

            var document = await context.OpenAsync(urlPrefix + productId);

            return await ConvertHtmlDocumentToProductAsync(productId, document);
            ;
        }

        private async Task<Product> ConvertHtmlDocumentToProductAsync(string productId, IDocument document)
        {
            var reviewsDocument = document.All.First(m => m.LocalName == "div" && m.HasAttribute("id") &&
                                                          m.GetAttribute("id").Equals("cm_cr-review_list"));
            var reviews = reviewsDocument.QuerySelectorAll("div.review").Select(review => new ProductReview
            {
                Title = review.QuerySelector("a.review-title").TextContent,
                Description = review.QuerySelector("span.review-text-content").QuerySelector("span").TextContent,
                RatingValue = Convert.ToDecimal(review.QuerySelector("i.review-rating").QuerySelector("span")
                    .TextContent.Substring(0, 3)),
                Customer = new Customer
                {
                    Name = review.QuerySelector("span.a-profile-name").TextContent
                }
            }).ToList();
            var product = new Product
            {
                ProductId = productId,
                Title = document.QuerySelector("a.a-link-normal").TextContent,
                Store = new Store
                {
                    Name = document.QuerySelector("a.a-size-base").TextContent
                },
                Reviews = reviews,
                CreatedDate = DateTime.Now
            };
            return await Task.FromResult(product);
        }
    }
}