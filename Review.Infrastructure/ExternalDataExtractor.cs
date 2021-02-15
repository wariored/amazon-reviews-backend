using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Review.Domain.Models;
using AngleSharp;
using AngleSharp.Html.Parser;


namespace Review.Infrastructure
{
    public class ExternalDataExtractor : IDataExtractor
    {
        public async Task<Product> GetExternalProductByIdAsync(string productId, string strUrl)
        {
            var context = BrowsingContext.New(Configuration.Default);

            var document = await context.OpenAsync(strUrl);

            throw new NotImplementedException();
        }
    }
}