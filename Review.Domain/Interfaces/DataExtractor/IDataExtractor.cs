using System.Threading.Tasks;
using Review.Domain.Models;

namespace Review.Domain.Interfaces.DataExtractor
{
    public interface IDataExtractor
    {
         Task<Product> GetExternalProductByIdAsync(string productId, string urlPrefix);
    }
}