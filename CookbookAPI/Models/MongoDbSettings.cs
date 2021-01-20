using System;

namespace CookbookAPI.Models
{
    public class MongoDbSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; } = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        public string DatabaseName { get; set; }=  Environment.GetEnvironmentVariable("DATABASE_NAME");
    }
}
