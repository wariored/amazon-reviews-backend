using System.Threading.Tasks;
using Review.Domain.Models;

namespace Review.Infrastructure
{
    public interface IDataExtractor
    {
        public Task<Product> GetExternalProductByIdAsync(string productId, string strUrl);
    }
}