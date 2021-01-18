namespace Cookbook_api.Models
{
    public class CookbookDatabaseSettings : ICookbookDatabaseSettings
    {
        public string RecipesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsersCollectionName { get; set; }
    }
}
