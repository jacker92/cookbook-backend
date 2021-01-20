using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CookbookAPI.Models.Requests
{
    public class CreateNewUserRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [JsonIgnore]
        [Required]
        public string Password { get; set; }
    }
}
