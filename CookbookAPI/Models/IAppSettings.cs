namespace CookbookAPI.Models
{
    public interface IAppSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
