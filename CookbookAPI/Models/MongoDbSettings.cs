namespace CookbookAPI.Models
{
    public class MongoDbSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
