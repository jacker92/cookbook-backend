namespace CookbookAPI.Models
{

    public interface ICookbookDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string RecipesCollectionName { get; set; }
        string UsersCollectionName { get; set; }
    }
}
