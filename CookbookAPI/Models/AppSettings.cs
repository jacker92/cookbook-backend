using System;

namespace CookbookAPI.Models
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; set; } = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        public string DatabaseName { get; set; }=  Environment.GetEnvironmentVariable("DATABASE_NAME");
        public string Secret { get; set; } = Environment.GetEnvironmentVariable("SECRET");
    }
}
