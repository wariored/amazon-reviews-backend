using Review.Domain.Interfaces.DBSettings;

namespace Review.Domain.Models
{
    public class ProductsDatabaseSettings: IProductsDatabaseSettings
    {
        public string ProductsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}