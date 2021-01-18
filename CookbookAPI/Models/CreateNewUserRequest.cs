using Newtonsoft.Json;

namespace CookbookAPI.Models
{
    public class CreateNewUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
