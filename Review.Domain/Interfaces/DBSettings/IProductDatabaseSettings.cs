namespace Review.Domain.Interfaces.DBSettings
{
    public interface IProductsDatabaseSettings
    {
        string ProductsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}